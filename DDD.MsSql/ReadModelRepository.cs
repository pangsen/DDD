using System;
using System.Collections.Generic;
using System.Data.Entity;
using DDD.Core.QueryService;

namespace DDD.MsSql
{
    public class ReadModelRepository<T> : IReadModeRepository<T> where T : ReadMode
    {
        private readonly ReadModelContext _readModelContext;
        public ReadModelRepository(IReadModelDbContextProvider contextProvider)
        {
            _readModelContext = contextProvider.GetReadModelDbContext<ReadModelContext>();
        }
        public T GetById(Guid id)
        {
            return _readModelContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _readModelContext.Set<T>();
        }

        public void Save(T t)
        {
            var entry = _readModelContext.Entry(t);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            _readModelContext.SaveChanges();
        }
    }
}