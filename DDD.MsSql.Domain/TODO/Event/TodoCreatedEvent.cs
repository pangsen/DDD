using System;
using DDD.Core;
using DDD.Core.Event;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.TODO.ReadModel;

namespace DDD.MsSql.Domain.TODO.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class TodoCreatedEvent : Core.Message.Event,ICreateEvent
    {
        public Guid Creater { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}