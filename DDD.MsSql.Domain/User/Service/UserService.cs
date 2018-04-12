using DDD.Core.Bus;
using DDD.Core.Repository;
using DDD.MsSql.Domain.TODO.Event;

namespace DDD.MsSql.Domain.User.Service
{
    public class UserService : DomainActionHelper<User>,
        IEventHandler<TodoCreatedEvent>
    {
        public UserService(IRepository<User> repository) : base(repository) { }
        public void Handle(TodoCreatedEvent @event)
        {
            ExecuteDomainUpdate(@event.Creater, user =>
            {
                user.NewTodoAdded(@event.AggregateId);
            });
        }
    }
}