using System;
using DDD.Core;
using DDD.Core.QueryService;

namespace DDD.Domain.User
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<UserReadModel>))]
    public class NewToDoAddedEvent : Core.Message.Event
    {
        public Guid ToDoId { get; set; }
    }
}