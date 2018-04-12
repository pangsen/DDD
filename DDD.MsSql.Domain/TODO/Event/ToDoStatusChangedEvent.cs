namespace DDD.MsSql.Domain.TODO.Event
{
    public class ToDoStatusChangedEvent : Core.Message.Event
    {
        public ToDoStatus Status { get; set; }

    }
}