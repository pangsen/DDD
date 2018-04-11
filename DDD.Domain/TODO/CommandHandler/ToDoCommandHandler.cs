using DDD.Core.Bus;
using DDD.Core.Repository;

namespace DDD.Domain
{
    public class ToDoCommandHandler : DomainActionHelper<ToDo>,
        ICommandHandler<CreateToDoCommand>,
        ICommandHandler<EditToDoCommand>,
        ICommandHandler<DoneToDoCommand>,
        ICommandHandler<ArchiveToDoCommand>
    {
        public ToDoCommandHandler(IRepository<ToDo> repository) : base(repository){}

        public void Handle(CreateToDoCommand message)
        {
            ExecuteDomainCreate(message.AggregateId, aggregate =>
            {
                aggregate.Create(message.Title, message.Description, message.UserId);
            });
        }

        public void Handle(EditToDoCommand message)
        {
            ExecuteDomainUpdate(message.AggregateId, aggregate =>
            {
                aggregate.Edit(message.Title, message.Description);
            });
        }

        public void Handle(DoneToDoCommand message)
        {
            ExecuteDomainUpdate(message.AggregateId, aggregate =>
            {
                aggregate.Done();
            });
        }

        public void Handle(ArchiveToDoCommand message)
        {
            ExecuteDomainUpdate(message.AggregateId, aggregate =>
            {
                aggregate.Archive();
            });
        }
    }
}