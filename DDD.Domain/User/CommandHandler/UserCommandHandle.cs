using DDD.Core.Bus;
using DDD.Core.Repository;

namespace DDD.Domain.User
{
    public class UserCommandHandle : DomainActionHelper<User>,
        ICommandHandler<UserRegisterCommand>
    {
        public UserCommandHandle(IRepository<User> repository) : base(repository) { }
        public void Handle(UserRegisterCommand message)
        {
            ExecuteDomainCreate(message.AggregateId, aggregate =>
            {
                aggregate.Registe(message.UserName, message.Password);
            });
        }


    }
}