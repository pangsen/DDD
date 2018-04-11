using DDD.Core;

namespace DDD.Domain
{
    public class ToDoStatus : ValueObject
    {
        public Status Status { get; set; }
    }
}