namespace DDD.MsSql
{
    public interface IAggregateDbContextProvider
    {
        T GetAggregateDbContext<T>() where T : AggregateDbContext;
    }
}