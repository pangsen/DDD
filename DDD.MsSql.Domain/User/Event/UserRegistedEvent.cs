using DDD.Core;
using DDD.Core.Event;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.User.ReadModel;

namespace DDD.MsSql.Domain.User.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<UserReadModel>))]
    public class UserRegistedEvent : Core.Message.Event, ICreateEvent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}