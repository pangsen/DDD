using System;

namespace DDD.Core.Repository
{
    public interface IRepository<T> where T : AggregateRoot
    {
        void Save(AggregateRoot aggregate);
        T GetById(Guid aggregateId);
    }
}