namespace DDD.MsSql
{
    public interface IReadModelDbContextProvider
    {
        T GetReadModelDbContext<T>() where T : ReadModelContext;
    }
}