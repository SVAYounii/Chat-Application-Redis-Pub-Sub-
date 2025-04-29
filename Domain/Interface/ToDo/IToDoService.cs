using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.ToDo
{
    public interface IToDoService
    {
        Task AddTodoAsync(string todo);
        Task<List<string>> GetAllTodosAsync();
    }
}
