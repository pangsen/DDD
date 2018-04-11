using DDD.Core;
using DDD.Core.Event;
using DDD.Core.QueryService;

namespace DDD.Domain.User
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<UserReadModel>))]
    public class UserRegistedEvent : Core.Message.Event, ICreateEvent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}