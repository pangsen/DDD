using System;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.Repository;

namespace DDD.Domain.CommandHandler
{
    public class PersonCreateCommandHandler :
        ICommandHandler<PersonCreateCommand>,
        ICommandHandler<PersonChangeNameCommand>
    {
        private readonly IRepository<Person> _repository;
        public PersonCreateCommandHandler(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public void Handle(PersonCreateCommand message)
        {
            var person = AggregateRootFactory.Build<Person>(message.AggregateId);
            person.Create(message.Name);
            _repository.Save(person);
        }
        public void Handle(PersonChangeNameCommand message)
        {
            var person = _repository.GetById(message.AggregateId);
            person.ChangeName(message.Name);
            _repository.Save(person);
        }
    }
}