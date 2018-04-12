using System;
using DDD.Core;
using DDD.MsSql.Domain.TODO.Event;

namespace DDD.MsSql.Domain.TODO
{
    public class ToDo : AggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public ToDoStatus Status { get; set; }
        public Guid Creater { get; set; }

        public void Create(string title, string description, Guid creater)
        {
            ApplyEvent(new TodoCreatedEvent { AggregateId = AggregateRootId, Title = title, Description = description, Creater = creater });
        }
        public void Edit(string title, string description)
        {
            ApplyEvent(new TodoEditedEvent { AggregateId = AggregateRootId, Title = title, Description = description });
        }
        public void Done()
        {
            ApplyEvent(new ToDoDoneEvent { AggregateId = AggregateRootId });
        }
        public void Archive()
        {
            ApplyEvent(new ToDoArchiveEvent { AggregateId = AggregateRootId });
        }

        public void Apply(TodoCreatedEvent @event)
        {
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