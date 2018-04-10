using System;

namespace DDD.Core.Message
{
    public abstract class Message
    {
        public Guid AggregateId { get; set; }
    }
}