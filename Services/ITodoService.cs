using ToDoListApi.Dtos;
using ToDoListApi.Entities;

namespace ToDoListApi.Services
{
    public interface ITodoService
    {
        Task<Todo> GetByIdAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<Todo> UpdateAsync(Guid id, UpdateTodoDto updateTodo);
        Task<Todo> CreateTodoAsync(Todo todo);
    }

}
