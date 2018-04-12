namespace DDD.MsSql
{
    public abstract class BaseDbContextProvider : IDbContextProvider
    {
        protected AggregateDbContext AggregateDbContext { get; set; }
        protected ReadModelContext ReadModelContext { get; set; }
        public T GetAggregateDbContext<T>() where T : AggregateDbContext
        {
            AggregateDbContext = AggregateDbContext ?? new AggregateDbContext();
            return AggregateDbContext as T;
        }
        public abstract T GetReadModelDbContext<T>() where T : ReadModelContext;
    }
}