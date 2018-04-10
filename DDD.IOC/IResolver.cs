using System;
using System.Collections.Generic;

namespace DDD.IOC
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type serviceType);
        IEnumerable<object> ResolveAll(Type serviceType);
        bool HasRegistrationFor<T>() where T : class;
    }
}