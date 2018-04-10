using System;
using DDD.Core.EventStore;

namespace DDD.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate)
        {
            _storage.Save<T>(aggregate.AggregateRootId, aggregate.UnCommitEvents);
            aggregate.UnCommitEvents.Clear();
        }

        public T GetById(Guid aggregateId)
        {
            var t = AggregateRootFactory.Build<T>(aggregateId);
            var events = _storage.GetEventsForAggregate<T>(aggregateId);
            t.LoadFromEvent(events);
            return t;
        }
    }
}