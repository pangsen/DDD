using System;
using DDD.Core.QueryService;

namespace DDD.Domain
{
    public class ToDoReadModel : ReadMode
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public ToDoStatus Status { get; set; }
        public Guid Creater { get; set; }

        public void Apply(TodoCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Title = @event.Title;
            Description = @event.Description;
            Creater = @event.Creater;
        }

        public void Apply(TodoEditedEvent @event)
        {
            Title = @event.Title;
            Description = @event.Description;
        }

        public void Apply(ToDoDoneEvent @event)
        {
            Status = @event.Status;
        }

        public void Apply(ToDoArchiveEvent @event)
        {
            Status = @event.Status;
        }
    }
}