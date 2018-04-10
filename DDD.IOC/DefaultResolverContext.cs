namespace DDD.IOC
{
    public class DefaultResolverContext: IResolverContext
    {
        public IResolver Resolver { get; }

        public DefaultResolverContext(IResolver resolver)
        {
            Resolver = resolver;
        }
    }
}