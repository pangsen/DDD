using System;
using System.Collections.Generic;

namespace DDD.Core
{
    public abstract class AggregateRoot
    {
        public List<Message.Event> UnCommitEvents { get; set; } = new List<Message.Event>();
        public Guid AggregateRootId { get; private set; }
        //Use for AggregateRootFactory
        private void SetAggregateRootId(Guid id)
        {
            AggregateRootId = id;
        }
        public void LoadFromEvent(List<Message.Event> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event, false);
            }
        }
        protected void ApplyEvent(Message.Event @event)
        {
            ApplyEvent(@event, true);
        }
        private void ApplyEvent(Message.Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) UnCommitEvents.Add(@event);
        }
    }
}