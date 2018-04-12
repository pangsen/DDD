using System.Data.Entity;

namespace DDD.MsSql
{
    public class AggregateDbContext : DbContext
    {
        public AggregateDbContext() : base("AggregateDbContext") { }
        public DbSet<Aggregate> Aggregates { get; set; }

    }
}