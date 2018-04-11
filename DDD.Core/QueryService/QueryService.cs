using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Core.Event;
using DDD.Core.QueryService;

namespace DDD.Core
{
    public class QueryService<T> where T : ReadMode, new()
    {
        private readonly IReadModeRepository<T> _repository;

        public QueryService(IReadModeRepository<T> repository)
        {
            _repository = repository;
        }
        public List<T> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public T GetById(Guid id)
        {
            return _repository.GetById(id);
        }
      
    }

    public class ReadModelEventHandler<T>  where T : ReadMode, new()
    {
        private readonly IReadModeRepository<T> _repository;

        public ReadModelEventHandler(IReadModeRepository<T> repository)
        {
            _repository = repository;
        }
        public void ApplyEvent(Message.Event @event)
        {
            T t;
            if (@event is ICreateEvent)
            {
                t = new T();
            }
            else
            {
                t = _repository.GetById(@event.AggregateId);
            }

            t.AsDynamic().Apply(@event);
            _repository.Save(t);
        }
    }
}