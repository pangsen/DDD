using System;
using System.Collections.Generic;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.EventStore;
using DDD.Core.Message;
using Newtonsoft.Json;

namespace DDD.MsSql
{
    public class EventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly AggregateDbContext _aggregateDbContext;
        public EventStore(IBus bus,IAggregateDbContextProvider contextProvider)
        {
            _bus = bus;
            _aggregateDbContext = contextProvider.GetAggregateDbContext<AggregateDbContext>();
        }

        public void Save<T>(Guid aggregateId, IEnumerable<Event> events) where T : AggregateRoot
        {
            var aggregate = _aggregateDbContext.Aggregates.Find(aggregateId);
            var isExist = aggregate != null;

            var aggregateEvents = isExist ? JsonConvert.DeserializeObject<List<Event>>(aggregate.Events,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }) :
                new List<Event>();
            aggregateEvents.AddRange(events);
            var jsonStr = JsonConvert.SerializeObject(aggregateEvents,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
            if (isExist)
            {
                aggregate.Events = jsonStr;
            }
            else
            {
                aggregate = new Aggregate { Id = aggregateId, Type = typeof(T).Name, Events = jsonStr };
                _aggregateDbContext.Aggregates.Add(aggregate);
            }

            _aggregateDbContext.SaveChanges();
            PublishEvents(events);
        }
        private void PublishEvents(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                _bus.Publish(@event);
            }
        }
        public List<Event> GetEventsForAggregate<T>(Guid aggregateId) where T : AggregateRoot
        {
            var jsonStr = _aggregateDbContext.Aggregates.Find(aggregateId)?.Events;
            if (string.IsNullOrEmpty(jsonStr))
            {
                return JsonConvert.DeserializeObject<List<Event>>(jsonStr,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            }
            throw new Exception("no event!");
        }
    }
}