using System;
using System.Collections.Generic;
using DDD.Core.Message;

namespace DDD.Mongo
{
    public class Aggregate
    {
        public Aggregate()
        {
            Events = new List<Event>();
        }
        public Guid Id { get; set; }
        public List<Event> Events { get; set; }

    }
}