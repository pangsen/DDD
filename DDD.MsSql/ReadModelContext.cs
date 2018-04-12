using System.Data.Entity;

namespace DDD.MsSql
{
    public class ReadModelContext : DbContext
    {
        public ReadModelContext(string contextName) : base(contextName) { }
    }

}
