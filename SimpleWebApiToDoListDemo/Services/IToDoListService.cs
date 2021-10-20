using SimpleWebApiToDoListDemo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWebApiToDoListDemo.Services
{
    public interface IToDoListService
    {
        Task<List<ToDoList>> GetRecordsAsync(DataFilter filter);
        Task<ToDoList> GetRecordAsync(long id);
        Task<ToDoList> InsertAsync(string item);
        Task<int> UpdateAsync(ToDoList todoItem);
        Task<int> DeleteAsync(long id);
    }
}
