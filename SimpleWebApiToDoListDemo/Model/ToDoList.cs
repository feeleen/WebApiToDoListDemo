using System;

namespace SimpleWebApiToDoListDemo.Model
{
    public class ToDoList : EntityBase<ToDoList>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
