using DDD.Core;

namespace DDD.MsSql.Domain.TODO
{
    public class ToDoStatus : ValueObject
    {
        public Status Status { get; set; }
    }
}