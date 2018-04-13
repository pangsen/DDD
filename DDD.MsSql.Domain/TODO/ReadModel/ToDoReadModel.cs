using System;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.TODO.Event;
using DDD.MsSql.Domain.User.ReadModel;

namespace DDD.MsSql.Domain.TODO.ReadModel
{
    public class ToDoReadModel : ReadMode
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid Creater { get; set; }

        public void Apply(TodoCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Title = @event.Title;
            Description = @event.Description;
            Creater = @event.Creater;
            CreateTime = DateTime.Now;
        }

        public void Apply(TodoEditedEvent @event)
        {
            Title = @event.Title;
            Description = @event.Description;
        }


    }
}