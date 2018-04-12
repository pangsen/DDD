using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace DDD.IOC.StructureMap
{
    public class StructureMapReslover : IResolver
    {
        private readonly Container _container;

        public StructureMapReslover(Container container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            
            return _container.GetInstance<T>();
        }

        public object Resolve(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public bool HasRegistrationFor<T>() where T : class
        {
            return _container.TryGetInstance<T>() != null;
        }
    }
}
