using System;
using DDD.Core;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.User.ReadModel;

namespace DDD.MsSql.Domain.User.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<UserReadModel>))]
    public class NewToDoAddedEvent : Core.Message.Event
    {
        public Guid ToDoId { get; set; }
    }
}