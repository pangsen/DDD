using System;
using DDD.Core;
using DDD.Core.Event;
using DDD.Core.QueryService;

namespace DDD.Domain
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class TodoCreatedEvent : Core.Message.Event,ICreateEvent
    {
        public Guid Creater { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}