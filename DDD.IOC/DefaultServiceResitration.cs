using System;
using System.Collections.Generic;

namespace DDD.IOC
{
    public class DefaultServiceResitration : IServiceRegistration
    {
        private readonly Dictionary<Type, List<Registration>> _registrations = new Dictionary<Type, List<Registration>>();
        private readonly object _syncRoot = new object();
       

        public void Register<TService, TImplementation>( Lifetime lifetime = Lifetime.AlwaysUnique, object[] parameters = null) where TService : class where TImplementation : class, TService
        {

            Register(typeof(TService),
                new ConstructorFactory(typeof(TImplementation), parameters), lifetime);
        }
        public void Register<TService>(Func<IResolverContext, TService> factory) where TService : class
        {
            Register(typeof(TService),
                new LambdaFactory<TService>(factory));
        }
        public void Register(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Register(serviceType, new ConstructorFactory(implementationType), lifetime);
        }
        public void RegisterType(Type serviceType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Register(serviceType, new ConstructorFactory(serviceType), lifetime);
        }
        public void RegisterGeneric(Type sreviceType, Type implementationType, Lifetime lifetime = Lifetime.AlwaysUnique, object[] parameters = null)
        {
            Register(sreviceType, new GenericFactory(implementationType, parameters), lifetime);
        }
        public void RegisterGenericType(Type sreviceType, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            Register(sreviceType, new GenericFactory(sreviceType), lifetime);
        }
        private void Register(Type serviceType, IFactory factory, Lifetime lifetime = Lifetime.AlwaysUnique)
        {
            lock (_syncRoot)
            {
                List<Registration> registrations;
                if (!_registrations.TryGetValue(serviceType, out registrations))
                {
                    registrations = new List<Registration>();
                    _registrations.Add(serviceType, registrations);
                }
                var registration = new Registration(
                    serviceType,
                    factory,
                    lifetime);
                registrations.Insert(0, registration);
            }

        }
        public IResolver CreateResolver()
        {
            return new DefaultResolver(_registrations);
        }

        public T GetContainer<T>() where T:class 
        {
            return _registrations as T;
        }
    }
}