using SimpleWebApiToDoListDemo.Model;
using SimpleWebApiToDoListDemo.Services;
using SimpleWebApiToDoListDemo.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListTests
{
    public class MockToDoListService : IToDoListService
    {
        public async Task<int> DeleteAsync(long id)
        {
            if (id < 0)
                throw new System.ArgumentException(nameof(id));

            return await Task.FromResult(1);
        }

        public async Task<ToDoList> GetRecordAsync(long id)
        {
            return await Task.FromResult(new ToDoList() { ID = 1, Name = "Test" });
        }

        public async Task<List<ToDoList>> GetRecordsAsync(DataFilter filter)
        {
            return await Task.FromResult(new List<ToDoList>() { new ToDoList() { ID = 1, Name = "Test" } });
        }

        public async Task<ToDoList> InsertAsync(string itemName)
        {
            return await Task.FromResult(new ToDoList() { ID = 1, Name = itemName });
        }

        public async Task<int> UpdateAsync(ToDoList todoItem)
        {
            return await Task.FromResult(1);
        }
    }
}
