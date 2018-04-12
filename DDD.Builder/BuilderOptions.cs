using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Core.EventStore;
using DDD.Core.Message;
using DDD.Core.QueryService;
using DDD.Core.Repository;
using DDD.IOC;
using DDD.LocalFile;
using IServiceRegistration = DDD.IOC.IServiceRegistration;

namespace DDD.Builder
{
    public class BuilderOptions
    {
        public IServiceRegistration ServiceRegistration { get; set; }
        public static BuilderOptions New()
        {
            return new BuilderOptions();
        }

        public BuilderOptions UseDefaultConfig()
        {
            UseDefaultRegistration();
            UseDefaultBus();
            UseLocalFileEventStore();
            UseLocalFileReadModeRepository();
            ServiceRegistration.RegisterGenericType(typeof(QueryService<>), Lifetime.Singleton);
            ServiceRegistration.RegisterGenericType(typeof(ReadModelEventHandler<>));
            ServiceRegistration.RegisterGeneric(typeof(IRepository<>), typeof(Repository<>));
            return this;
        }
        public BuilderOptions UseDefaultRegistration()
        {
            if (ServiceRegistration == null)
            {
                ServiceRegistration = new DefaultServiceResitration();
            }
            ServiceRegistration.Register(x => ServiceRegistration);
            ServiceRegistration.Register(x => ServiceRegistration.CreateResolver());

            return this;
        }
        public BuilderOptions UseDefaultBus()
        {
            ServiceRegistration.Register<IBus, DefaultBus>(lifetime: Lifetime.Singleton);
            return this;
        }
        public BuilderOptions UseLocalFileEventStore()
        {
            ServiceRegistration.Register<IEventStore, LocalFileEventStore>(Lifetime.Singleton);
            return this;
        }
        public BuilderOptions UseLocalFileReadModeRepository()
        {
            ServiceRegistration.RegisterGeneric(typeof(IReadModeRepository<>), typeof(LocalFileReadModeRepository<>), Lifetime.Singleton);
            return this;
        }
        public BuilderOptions Register(Assembly assembly)
        {
            RegisterCommandHandler(assembly);
            RegisterEventHandler(assembly);
            return this;
        }
        public BuilderOptions RegisterCommandHandler(Assembly assembly)
        {
            var types = assembly.GetTypes()
                 .Where(a => a.GetTypeInfo().GetInterfaces()
                 .Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (var type in types)
            {
                var handlers = type.GetInterfaces()
                    .Where(a => a.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                foreach (var handler in handlers)
                {
                    ServiceRegistration.Register(typeof(ICommandHandler<>).MakeGenericType(handler.GetGenericArguments()), type);
                }
            }
            return this;
        }
        public BuilderOptions RegisterCommandHandler<T>(ICommandHandler<T> commandHandler)
            where T : Command
        {
            ServiceRegistration.Register(typeof(ICommandHandler<T>), commandHandler.GetType());
            return this;
        }
        public BuilderOptions RegisterEventHandler(Assembly assembly)
        {
            var types = assembly.GetTypes()
                 .Where(a => a.GetTypeInfo().GetInterfaces()
                 .Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)));

            foreach (var type in types)
            {
                var handlers = type.GetInterfaces()
                    .Where(a => a.GetGenericTypeDefinition() == typeof(IEventHandler<>));
                foreach (var handler in handlers)
                {
                    ServiceRegistration.Register(typeof(IEventHandler<>).MakeGenericType(handler.GetGenericArguments()), type);
                }
            }
            return this;
        }
        public BuilderOptions RegisterEventHandler<T>(IEventHandler<T> eventHandler)
         where T : Event
        {
            ServiceRegistration.Register(typeof(IEventHandler<T>), eventHandler.GetType());
            return this;
        }
    }
}