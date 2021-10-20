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
        public static List<ToDoList> tempStorage = new List<ToDoList>();
        public MockToDoListService()
        {
            tempStorage.Add(new ToDoList() { ID = 1, Name = "First" });
        }

        public async Task<int> DeleteAsync(long id)
        {
            if (id < 0)
                throw new System.ArgumentException(nameof(id));

            if (id > 1000)
            {
                return await Task.FromResult(0);
            }

            return await Task.FromResult(1);
        }

        public async Task<ToDoList> GetRecordAsync(long id)
        {
            return await Task.FromResult(tempStorage.Where(x=> x.ID == id).FirstOrDefault());
        }

        public async Task<List<ToDoList>> GetRecordsAsync(DataFilter filter)
        {
            return await Task.FromResult(tempStorage);
        }

        public async Task<ToDoList> InsertAsync(string itemName)
        {
            var newItem = new ToDoList() { ID = 100, Name = itemName };
            tempStorage.Add(newItem);
            return await Task.FromResult(newItem);
        }

        public async Task<int> UpdateAsync(ToDoList todoItem)
        {
            return await Task.FromResult(1);
        }
    }
}
