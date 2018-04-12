
using System;
using DDD.Builder;
using DDD.Core.EventStore;
using DDD.Core.QueryService;
using DDD.IOC;

namespace DDD.MsSql
{
    public static class BuilderOptionsExtension
    {
        public static BuilderOptions UseMsSqlPersistent(this BuilderOptions options, Func<IResolverContext, IDbContextProvider> dbProviderFactory)
        {
            options.ServiceRegistration.Register<IEventStore, EventStore>(Lifetime.Singleton);
            options.ServiceRegistration.RegisterGeneric(typeof(IReadModeRepository<>), typeof(ReadModelRepository<>), Lifetime.Singleton);
            options.ServiceRegistration.Register(dbProviderFactory);
            options.ServiceRegistration.Register<IReadModelDbContextProvider>(dbProviderFactory);
            options.ServiceRegistration.Register<IAggregateDbContextProvider>(dbProviderFactory);
            return options;
        }
    }
}