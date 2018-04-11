using System.Linq;
using System.Reflection;
using DDD.Core.Bus;
using DDD.Core.EventStore;
using DDD.Core.QueryService;
using DDD.Core.Repository;
using DDD.IOC;

namespace DDD.Core
{
    public class BuilderOption
    {
        public IServiceRegistration ServiceRegistration { get; set; }
        public static BuilderOption New()
        {
            return new BuilderOption(new DefaultServiceResitration());
        }

        private BuilderOption(IServiceRegistration registration)
        {
            ServiceRegistration = registration;
            ServiceRegistration.Register(x => registration);
            ServiceRegistration.Register<IBus, DefaultBus>(lifetime: Lifetime.Singleton);
            ServiceRegistration.Register(x => ServiceRegistration.CreateResolver());
            ServiceRegistration.Register<IEventStore, LocalFileEventStore>(new object[] { ServiceRegistration.CreateResolver().Resolve<IBus>(), "D:\\temp" }, Lifetime.Singleton);
            ServiceRegistration.RegisterGenericType(typeof(QueryService<>), Lifetime.Singleton);
            ServiceRegistration.RegisterGenericType(typeof(ReadModelEventHandler<>));
            ServiceRegistration.RegisterGeneric(typeof(IRepository<>), typeof(Repository<>));
            ServiceRegistration.RegisterGeneric(typeof(IReadModeRepository<>), typeof(LocalFileReadModeRepository<>), new object[1] { "D:\\temp" }, Lifetime.Singleton);
        }

        public BuilderOption RegisterDefault(Assembly assembly)
        {
            RegisterCommandHandler(assembly);
            RegisterEventHandler(assembly);
            return this;
        }
        public BuilderOption RegisterCommandHandler(Assembly assembly)
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
        public BuilderOption RegisterEventHandler(Assembly assembly)
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
    }
}