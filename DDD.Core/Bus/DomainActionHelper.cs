using System;
using DDD.Core.Repository;

namespace DDD.Core.Bus
{
    public class DomainActionHelper<T> where T : AggregateRoot
    {
        private readonly IRepository<T> _repository;
        public DomainActionHelper(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void ExecuteDomainUpdate(Guid aggregateId, Action<T> action)
        {
            var aggregate = _repository.GetById(aggregateId);
            action(aggregate);
            _repository.Save(aggregate);
        }
        public void ExecuteDomainCreate(Guid aggregateId, Action<T> action)
        {
            var aggregate = AggregateRootFactory.Build<T>(aggregateId);
            action(aggregate);
            _repository.Save(aggregate);
        }

    }
}