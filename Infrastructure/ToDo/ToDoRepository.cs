using Domain.Interface.ToDo;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ToDo
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly IDatabase _db;

        public ToDoRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<List<string>> GetAllTodosAsync()
        {
            var todos = await _db.ListRangeAsync("todo_list");
            return todos.Select(x => x.ToString()).ToList();
        }

        public async Task AddTodoAsync(string todo)
        {
            await _db.ListLeftPushAsync("todo_list", todo);
        }

       
    }
}
