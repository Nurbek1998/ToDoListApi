using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoListApi.Dtos;
using ToDoListApi.Entities;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await todoService.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            try
            {
                var todo = await todoService.GetByIdAsync(id);
                return Ok(todo);
            }
            catch (ArgumentException)
            {
                return NotFound($"Todo with ID {id} not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodo)
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (subClaim is null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(subClaim.Value);
            var toDo = new Todo
            {
                Title = createTodo.Title,
                Description = createTodo.Description,
                UserId = userId
            };

            var createdTodo = await todoService.CreateTodoAsync(toDo);
            return Ok(createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] UpdateTodoDto updateTodo)
        {
            var updatedTodo = await todoService.UpdateAsync(id, updateTodo);
            if (updatedTodo is null)
            {
                return NotFound($"Todo with ID {id} not found.");
            }
            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTodo(Guid id)
        {
            var isDeleted = await todoService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Todo with ID {id} not found.");
            }

            return Ok(true);
        }
    }
}
