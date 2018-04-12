using System;
using StructureMap;
using StructureMap.Pipeline;

namespace DDD.IOC.StructureMap
{
    public static class LifecyclesExtension
    {
        public static ILifecycle ToLifecycles(this Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    return Lifecycles.Singleton;
                case Lifetime.AlwaysUnique:
                    return Lifecycles.Unique;
                case Lifetime.PerHttpContext:
                    return Lifecycles.ThreadLocal;
                case Lifetime.PerThread:
                    return Lifecycles.ThreadLocal;
            }
            throw new Exception("not support!");
        }
    }
    public class StructureMapRegistration : IServiceRegistration
    {
        private Container Container { get; set; }
        public StructureMapRegistration(Container container)
        {
            Container = container ?? new Container();
        }

        public T GetContainer<T>() where T : class
        {
            return Container as T;
        }

        public void Register<TService, TImplementation>(Lifetime lifetime = Lifetime.AlwaysUnique, object[] paramters = null) where TService : class where TImplementation : class, TService
        {
            Container.Configure(c => c.For<TService>().Use<TImplementation>().SetLifecycleTo(lifetime.ToLifecycles()));
        }

        public void Register<TService>(Func<IResolverContext, TService> factory) where TService : class
        {
            Container.Configure(c => c.For<TService>().Use(x => factory(new DefaultResolverContext(CreateResolver()))));
        }

        public void Register(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Container.Configure(c => c.For(serviceType).Use(implementationType).SetLifecycleTo(lifetime.ToLifecycles()));
        }

        public void RegisterType(Type serviceType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Container.Configure(c => c.For(serviceType, lifetime.ToLifecycles()));
        }

        public void RegisterGeneric(Type sreviceType, Type implementationType,
            Lifetime lifetime = Lifetime.AlwaysUnique, object[] paramters = null)
        {
            Container.Configure(c => c.For(sreviceType).Use(implementationType).SetLifecycleTo(lifetime.ToLifecycles()));
        }

        public void RegisterGenericType(Type sreviceType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Container.Configure(c => c.For(sreviceType, lifetime.ToLifecycles()));
        }

        public IResolver CreateResolver()
        {
            return new StructureMapReslover(Container);
        }
    }
}