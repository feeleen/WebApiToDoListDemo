using LinqToDB;
using SimpleWebApiToDoListDemo.Data;
using SimpleWebApiToDoListDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApiToDoListDemo.Services
{
    public class ToDoListService : IToDoListService
    {
        public async Task<List<ToDoList>> GetRecordsAsync(DataFilter filter)
        {
            using (var db = new DbContext())
            {
                var result = db.GetTable<ToDoList>().Where(x => 1 == 1);

                switch (filter.State)
                {
                    case ItemState.NotCompleted:
                        result = result.Where(x => x.IsComplete == false);
                        break;

                    case ItemState.Completed:
                        result = result.Where(x => x.IsComplete == true);
                        break;

                    case ItemState.All:
                        break;
                }

                if (filter.DateCreatedFrom > DateTime.MinValue)
                {
                    result = result.Where(x => x.DateCreated >= filter.DateCreatedFrom.Date);
                }

                if (filter.DateCreatedTo > DateTime.MaxValue)
                {
                    result = result.Where(x => x.DateCreated >= filter.DateCreatedTo.Date);
                }

                return await result.ToListAsync();
            }
        }

        public async Task<ToDoList> GetRecordAsync(long id)
        {
            using (var db = new DbContext())
            {
                var todoItem = await db.GetTable<ToDoList>().FirstOrDefaultAsync(x => x.ID == id);

                return todoItem;
            }
        }

        public async Task<ToDoList> InsertAsync(string itemName)
        {
            using (var db = new DbContext())
            {
                if (string.IsNullOrWhiteSpace(itemName))
                    throw new ArgumentNullException($"{nameof(itemName)} can't be empty");

                var res = Convert.ToInt64(await db.InsertWithIdentityAsync(new ToDoList() { Name = itemName }));

                if (res > 0)
                {
                    var newItem = db.GetTable<ToDoList>().Where(x => x.ID == res).FirstOrDefault();
                    return newItem;
                }
                else
                {
                    throw new Exception($"Rows inserted: {res}");
                }
            }
        }

        public async Task<int> UpdateAsync(ToDoList todoItem)
        {
            using (var db = new DbContext())
            {
                if (todoItem == null)
                {
                    throw new ArgumentNullException(nameof(todoItem));
                }

                var res = await db.UpdateAsync(todoItem);

                return res;
            }
        }
        
        
        public async Task<int> DeleteAsync(long id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            using (var db = new DbContext())
            {
                var res = await db.GetTable<ToDoList>().DeleteAsync(x => x.ID == id);

                return res;
            }
        }
    }
}
