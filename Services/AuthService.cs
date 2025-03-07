using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListApi.Data;
using ToDoListApi.Dtos;
using ToDoListApi.Entities;

namespace ToDoListApi.Services;
public class AuthService(ToDoListDbContext context, IConfiguration configuration) : IAuthService
{
    public async Task<string> LoginAsync(LoginUser loginUser)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == loginUser.Username);

        if (user == null)
            throw new InvalidOperationException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        return GenerateJwtToken(user);
    }

    public async Task<User> RegisterAsync(CreateUser createUser)
    {
        if (createUser.Username == null || await context.Users.FirstOrDefaultAsync(u => u.Username == createUser.Username) != null)
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = createUser.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.PasswordHash)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }
    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("JwtConfig:TokenValidityMins", 30)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

