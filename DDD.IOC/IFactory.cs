using System;

namespace DDD.IOC
{
    public interface IFactory
    {
        object Create(IResolverContext resolverContext, Type[] genericTypeArguments);
    }
}