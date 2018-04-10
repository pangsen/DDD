using System;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.QueryService;

namespace DDD.Domain.Event
{
    [ReadModelUpdater(typeof(ReadModelUpdater<PersonReadMode>))]
    public class PersonNameEvent : Core.Message.Event
    {
        public string Name { get; set; }
    }



    public class EventHandler : IEventHandler<PersonNameEvent>
    {
        public void Handle(PersonNameEvent @event)
        {
            Console.WriteLine($"-----------{@event.Name}-------------");
        }
    }
}