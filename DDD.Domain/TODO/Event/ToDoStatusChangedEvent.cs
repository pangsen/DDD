namespace DDD.Domain
{
    public class ToDoStatusChangedEvent : Core.Message.Event
    {
        public ToDoStatus Status { get; set; }

    }
}