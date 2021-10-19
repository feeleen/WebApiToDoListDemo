using LinqToDB;
using SimpleWebApiToDoListDemo.Data;
using SimpleWebApiToDoListDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApiToDoListDemo.Services
{
    public class ToDoListService
    {
        public static async Task<List<ToDoList>> GetRecordsAsync(DataFilter filter)
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

        public static async Task<ToDoList> GetRecordAsync(long id)
        {
            using (var db = new DbContext())
            {
                var todoItem = await db.GetTable<ToDoList>().FirstOrDefaultAsync(x => x.ID == id);

                return todoItem;
            }
        }

        public static async Task<ToDoList> InsertAsync(string item)
        {
            using (var db = new DbContext())
            {
                var res = Convert.ToInt64(await db.InsertWithIdentityAsync(new ToDoList() { Name = item }));

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

        public static async Task<int> UpdateAsync(ToDoList todoItem)
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
        
        
        public static async Task<int> DeleteAsync(long id)
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
