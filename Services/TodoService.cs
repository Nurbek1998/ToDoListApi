using Microsoft.EntityFrameworkCore;
using System.Threading;
using ToDoListApi.Data;
using ToDoListApi.Dtos;
using ToDoListApi.Entities;

namespace ToDoListApi.Services
{
    public class TodoService(ToDoListDbContext context) : ITodoService
    {
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            var createdTodo = await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return createdTodo.Entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existTodo = await context.Todos.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (existTodo == null)
            {
                return false;
            }

            context.Todos.Remove(existTodo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await context.Todos.AsNoTracking().ToListAsync();
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            return await context.Todos.Where(t => t.Id == id).FirstOrDefaultAsync() ?? throw new ArgumentException();
        }

        public async Task<Todo> UpdateAsync(Guid id, UpdateTodoDto updateTodo)
        {
            var existTodo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (existTodo is null)
            {
                return null!;
            }

            existTodo.Title = updateTodo.Title;
            existTodo.Description = updateTodo.Description;
            existTodo.IsCompleted = updateTodo.IsCompleted;

            await context.SaveChangesAsync();

            return existTodo;
        }
    }
}
