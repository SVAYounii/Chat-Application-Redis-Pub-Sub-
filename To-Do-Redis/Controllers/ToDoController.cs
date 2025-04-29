using Domain.Interface.ToDo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace To_Do_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IConfiguration _configuration;

        public ToDoController(IToDoService toDoService, IConfiguration configuration)
        {
            _toDoService = toDoService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _toDoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] string todo)
        {
            if (string.IsNullOrWhiteSpace(todo))
            {
                return BadRequest("Todo cannot be empty.");
            }

            await _toDoService.AddTodoAsync(todo);
            return Ok();
        }
    }
}
