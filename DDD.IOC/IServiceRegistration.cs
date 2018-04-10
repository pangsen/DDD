using System;

namespace DDD.IOC
{
    public interface IServiceRegistration
    {
        void Register<TService, TImplementation>(object[] paramters=null, Lifetime lifetime =Lifetime.AlwaysUnique)
            where TImplementation : class, TService
            where TService : class;
        void Register<TService>(Func<IResolverContext, TService> factory) where TService : class;
        void Register(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique);
        void RegisterType(Type serviceType, Lifetime lifetime = Lifetime.AlwaysUnique);
        void RegisterGeneric(Type sreviceType, Type implementationType, object[] paramters = null, Lifetime lifetime = Lifetime.AlwaysUnique);
        void RegisterGenericType(Type sreviceType, Lifetime lifetime = Lifetime.AlwaysUnique);
        IResolver CreateResolver();
    }
}