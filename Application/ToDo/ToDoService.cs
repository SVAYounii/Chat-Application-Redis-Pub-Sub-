using Domain.Interface.ToDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<List<string>> GetAllTodosAsync()
        {
            return await _toDoRepository.GetAllTodosAsync();
        }

        public async Task AddTodoAsync(string todo)
        {
            if (string.IsNullOrWhiteSpace(todo))
                throw new ArgumentException("Todo cannot be empty.");

            await _toDoRepository.AddTodoAsync(todo);
        }
    }
}
