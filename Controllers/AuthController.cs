using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Dtos;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService,
        IValidator<CreateUser> createUserValidator,
        IValidator<LoginUser> loginUserValidator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUser createUser)
        {
            var validationResult = await createUserValidator.ValidateAsync(createUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _ = await authService.RegisterAsync(createUser);
            return Created(uri: $"api/register/{createUser.Username}", value: createUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var validationResult = await loginUserValidator.ValidateAsync(loginUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var token = await authService.LoginAsync(loginUser);
            return Ok(new { Token = token });
        }
    }
}
