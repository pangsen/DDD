using System;
using DDD.Core;
using DDD.Domain.Event;

namespace DDD.Domain
{
    public class Person : AggregateRoot
    {
        public string Name { get; set; }


        private Person(Guid id) : base(id) { }

        public void Create(string name)
        {
            ApplyEvent(new PersonCreatedEvent { AggregateId = AggregateRootId, Name = name });
        }
        public void ChangeName(string name)
        {
            ApplyEvent(new PersonNameEvent { AggregateId = AggregateRootId, Name = name });
        }

        public void Apply(PersonCreatedEvent @event)
        {
            Name = @event.Name;
        }
        public void Apply(PersonNameEvent @event)
        {
            Name = @event.Name;
        }
    }
}