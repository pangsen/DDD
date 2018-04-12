using DDD.Core.Bus;
using DDD.Core.Repository;
using DDD.MsSql.Domain.User.Command;

namespace DDD.MsSql.Domain.User.CommandHandler
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