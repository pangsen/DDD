using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DDD.Core.Bus;
using Newtonsoft.Json;

namespace DDD.Core.EventStore
{
    public class LocalFileEventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly string _dir;


        public LocalFileEventStore(IBus bus, string dir)
        {
            _bus = bus;
            _dir = dir;
        }

        public void Save<T>(Guid aggregateId, IEnumerable<Message.Event> events) where T : AggregateRoot
        {
            List<Message.Event> aggregateEvents = new List<Message.Event>();
            var path = $"{_dir}\\{typeof(T).Name}_{aggregateId}";
            if (File.Exists(path))
            {
                var jsonSrt = File.ReadAllText(path);
                aggregateEvents = JsonConvert.DeserializeObject<IEnumerable<Message.Event>>(jsonSrt,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }).ToList();
            }
            aggregateEvents.AddRange(events);
            var stringValue = JsonConvert.SerializeObject(aggregateEvents,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
            File.WriteAllText(path, stringValue);
            PublishEvents(events);
        }

        private void PublishEvents(IEnumerable<Message.Event> events)
        {
            foreach (var @event in events)
            {
                _bus.Publish(@event);
            }
        }
        public List<Message.Event> GetEventsForAggregate<T>(Guid aggregateId) where T : AggregateRoot
        {
            var path = $"{_dir}\\{typeof(T).Name}_{aggregateId}";
            var jsonSrt = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<Message.Event>>(jsonSrt,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }).ToList();
        }
    }
}