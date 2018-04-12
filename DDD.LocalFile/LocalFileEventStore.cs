using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.EventStore;
using DDD.Core.Message;
using Newtonsoft.Json;

namespace DDD.LocalFile
{
    public class LocalFileEventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly string _dir;


        public LocalFileEventStore(IBus bus)
        {
            _bus = bus;
            _dir = ConfigurationManager.AppSettings["EventStoreFilePath"]; ;
        }

        public void Save<T>(Guid aggregateId, IEnumerable<Event> events) where T : AggregateRoot
        {
            List<Event> aggregateEvents = new List<Event>();
            var path = $"{_dir}\\{typeof(T).Name}_{aggregateId}";
            if (File.Exists(path))
            {
                var jsonSrt = File.ReadAllText(path);
                aggregateEvents = JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonSrt,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }).ToList();
            }
            aggregateEvents.AddRange(events);
            var stringValue = JsonConvert.SerializeObject(aggregateEvents,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
            File.WriteAllText(path, stringValue);
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
            var path = $"{_dir}\\{typeof(T).Name}_{aggregateId}";
            var jsonSrt = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonSrt,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }).ToList();
        }
    }
}