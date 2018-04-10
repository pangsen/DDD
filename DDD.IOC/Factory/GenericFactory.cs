using System;
using System.Linq;
using System.Reflection;

namespace DDD.IOC
{
    public class GenericFactory : IFactory
    {
        private readonly Type _serviceType;
        private readonly object[] _parameters;
        public GenericFactory(Type serviceType, object[] parameters=null)
        {
            _parameters = parameters;
            var constructorInfos = serviceType.GetTypeInfo().GetConstructors();
            if (constructorInfos.Length > 1)
            {
                throw new Exception($"Type {serviceType.Name} has more than one constructor");
            }
            _serviceType = serviceType;
        }
        public object Create(IResolverContext resolverContext, Type[] genericTypeArguments)
        {
            var genericType = _serviceType.MakeGenericType(genericTypeArguments);
            var constructorInfo = genericType.GetTypeInfo().GetConstructors().Single();

            if (_parameters != null)
            {
                return constructorInfo.Invoke(_parameters);
            }

            var parameterInfos = constructorInfo.GetParameters();

            var parameters = new object[parameterInfos.Length];
            foreach (var parameterInfo in parameterInfos)
            {
                parameters[parameterInfo.Position] = resolverContext.Resolver.Resolve(parameterInfo.ParameterType);
            }
            return constructorInfo.Invoke(parameters);
        }
    }
}