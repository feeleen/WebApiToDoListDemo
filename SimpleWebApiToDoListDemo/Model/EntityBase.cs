using LinqToDB.Mapping;

namespace SimpleWebApiToDoListDemo.Model
{
    public class EntityBase<T> where T : EntityBase<T>
    {
        [Column]
        [Identity]
        [PrimaryKey]
        public long ID { get; set; }
    }
}
