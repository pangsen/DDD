using System.Data.Entity;
using DDD.MsSql.Domain.TODO.ReadModel;
using DDD.MsSql.Domain.User.ReadModel;

namespace DDD.MsSql.Domain.DbContext
{
    public class ReadModelDbContext : ReadModelContext
    {
        public ReadModelDbContext() : base("ReadModelDbContext")
        {
        }

        public DbSet<UserReadModel> Users { get; set; }

        public DbSet<ToDoReadModel> Todos { get; set; }

    }


    public class DbContextProvider : BaseDbContextProvider
    {
        public override T GetReadModelDbContext<T>()
        {
            ReadModelContext = ReadModelContext ?? new ReadModelDbContext();
            return ReadModelContext as T;
        }
    }
}
