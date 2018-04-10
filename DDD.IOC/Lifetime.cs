namespace DDD.IOC
{
    public enum Lifetime
    {
        AlwaysUnique,
        Singleton,
        PerThread,
        PerHttpContext
    }
}