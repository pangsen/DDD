using System;
using System.Collections.Generic;

namespace DDD.IOC
{
    public class DefaultServiceResitration : IServiceRegistration
    {
        private readonly Dictionary<Type, List<Registration>> _registrations = new Dictionary<Type, List<Registration>>();
        private readonly object _syncRoot = new object();
        public void Register<TService, TImplementation>(object[] parameters=null, Lifetime lifetime = Lifetime.AlwaysUnique) where TService : class where TImplementation : class, TService
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
        public void RegisterGeneric(Type sreviceType, Type implementationType,object[] parameters=null, Lifetime lifetime = Lifetime.AlwaysUnique)
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
    }
}