using System;
using System.Collections.Generic;
using System.Reflection;

namespace DDD.Core
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot(Guid id)
        {
            AggregateRootId = id;
        }
        public List<Message.Event> UnCommitEvents { get; set; } = new List<Message.Event>();
        public Guid AggregateRootId { get; private set; }
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


    public class AggregateRootFactory
    {
        public static T Build<T>(Guid id) where T : AggregateRoot
        {
            var type = typeof(T);
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Guid) }, null);
            if (constructor == null)
            {
                throw new Exception("no private constructor.");
            }
            return constructor.Invoke(new object[] { id }) as T;
        }
    }
}