using System;

namespace DDD.IOC
{
    public interface IServiceRegistration
    {
        T GetContainer<T>() where T : class;
        void Register<TService, TImplementation>( Lifetime lifetime =Lifetime.AlwaysUnique, object[] parameters = null)
            where TImplementation : class, TService
            where TService : class;
        void Register<TService>(Func<IResolverContext, TService> factory) where TService : class;
        void Register(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique);
        void RegisterType(Type serviceType, Lifetime lifetime = Lifetime.AlwaysUnique);
        void RegisterGeneric(Type sreviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique, object[] paramters = null);
        void RegisterGenericType(Type sreviceType, Lifetime lifetime = Lifetime.AlwaysUnique);
        IResolver CreateResolver();
    }
}