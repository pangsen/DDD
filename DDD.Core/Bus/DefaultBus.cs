using System;
using System.Linq;
using DDD.Core.Message;
using DDD.Core.QueryService;
using IResolver = DDD.IOC.IResolver;

namespace DDD.Core.Bus
{
    public class DefaultBus : IBus
    {
        private readonly IResolver _resolver;
        public DefaultBus(IResolver resolver)
        {
            _resolver = resolver;
        }

        public void Send<T>(T command) where T : Command
        {
            var handler = _resolver.Resolve<ICommandHandler<T>>();
            if (handler != null)
            {
                handler.Handle(command);
            }
            else
            {
                throw new Exception("no handler registered.");
            }
        }

        public void Publish<T>(T @event) where T : Message.Event
        {
            ApplyEventToUpdateReadModel(@event);
            ApplyEvent(@event);
        }

        private void ApplyEventToUpdateReadModel<T>(T @event) where T : Message.Event
        {
            foreach (var customAttribute in @event.GetType().GetCustomAttributes(typeof(ReadModelEventHandlerRegisterAttribute), true))
            {
                var readModelUpdaterType = ((ReadModelEventHandlerRegisterAttribute)customAttribute).ReadModelUpdaterType;
                var updater = _resolver.Resolve(readModelUpdaterType);
                updater.AsDynamic().ApplyEvent(@event);
            }
        }
        private void ApplyEvent<T>(T @event) where T : Message.Event
        {
            var handlers = _resolver.ResolveAll(typeof(IEventHandler<>).MakeGenericType(@event.GetType()));

            if (handlers != null && handlers.Any())
            {
                foreach (var handler in handlers)
                {
                    handler.AsDynamic().Handle(@event);
                }
            }
        }
    }
}