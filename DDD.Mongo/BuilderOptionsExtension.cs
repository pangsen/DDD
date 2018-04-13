using System;
using DDD.Builder;
using DDD.Core.EventStore;
using DDD.Core.QueryService;
using DDD.IOC;

namespace DDD.Mongo
{
    public static class BuilderOptionsExtension
    {
        public static BuilderOptions UseMongoPersistent(this BuilderOptions options)
        {
            options.ServiceRegistration.Register<IEventStore, MongoEventStore>(Lifetime.Singleton);
            options.ServiceRegistration.RegisterGeneric(typeof(IReadModeRepository<>), typeof(MongoReadModeRepository<>), Lifetime.Singleton);
            return options;
        }
    }
}