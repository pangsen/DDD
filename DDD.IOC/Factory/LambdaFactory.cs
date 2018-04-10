using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DDD.IOC
{
    public class LambdaFactory<TService> : IFactory
    {
        private readonly Func<IResolverContext, TService> _factory;
        public LambdaFactory(Func<IResolverContext, TService> factory)
        {
            _factory = factory;
        }
        public object Create(IResolverContext resolverContext, Type[] genericTypeArguments)
        {
            return _factory(resolverContext);
        }
    }
}
