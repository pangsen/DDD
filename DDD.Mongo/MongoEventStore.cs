using System;
using System.Collections.Generic;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.EventStore;
using DDD.Core.Message;
using MongoDB.Driver;

namespace DDD.Mongo
{
    public class MongoEventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly IMongoDatabase _database;
        public MongoEventStore(IBus bus)
        {
            _bus = bus;
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("test");

        }

        private IMongoCollection<Aggregate> GetCollection<T>()
        {
            return _database.GetCollection<Aggregate>(typeof(T).Name);
        }
        public void Save<T>(Guid aggregateId, IEnumerable<Event> events) where T : AggregateRoot
        {
            var collection = GetCollection<T>();
            var filter = Builders<Aggregate>.Filter.Eq(a => a.Id, aggregateId);
            var aggregate = collection.Find(filter).FirstOrDefault() ?? new Aggregate { Id = aggregateId };
            aggregate.Events.AddRange(events);
            collection.ReplaceOne(filter, aggregate, new UpdateOptions { IsUpsert = true });
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
            var collection = GetCollection<T>();
            var filter = Builders<Aggregate>.Filter.Eq(a => a.Id, aggregateId);
            return collection.Find(filter).FirstOrDefault()?.Events ?? new List<Event>();
        }
    }
}