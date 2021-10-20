using LinqToDB.Mapping;
using System;

namespace SimpleWebApiToDoListDemo.Model
{
    [Table("ToDoList")]
    public class ToDoList : EntityBase<ToDoList>
    {
        [Column]
        public string Name { get; set; }
        [Column]
        public bool IsComplete { get; set; }
        [Column]
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
