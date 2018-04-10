using System;
using System.Runtime.InteropServices.ComTypes;
using DDD.Core;
using DDD.Core.Event;
using DDD.Core.QueryService;

namespace DDD.Domain.Event
{
    [ReadModelUpdater(typeof(ReadModelUpdater<PersonReadMode>))]
    public class PersonCreatedEvent : Core.Message.Event, ICreateEvent
    {
        public string Name { get; set; }
    }
   
}