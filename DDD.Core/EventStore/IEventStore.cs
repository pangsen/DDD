using System;
using System.Collections.Generic;

namespace DDD.Core.EventStore
{
    public interface IEventStore
    {

        void Save<T>(Guid aggregateId, IEnumerable<Message.Event> events) where T : AggregateRoot;
        List<Message.Event> GetEventsForAggregate<T>(Guid aggregateId) where T : AggregateRoot;

    }
}