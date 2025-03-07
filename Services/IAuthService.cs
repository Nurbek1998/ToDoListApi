using ToDoListApi.Dtos;
using ToDoListApi.Entities;

namespace ToDoListApi.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(CreateUser createUser);
        Task<string> LoginAsync(LoginUser loginUser);
    }
}
