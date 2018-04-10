using System;
using System.Collections.Generic;

namespace DDD.Core.QueryService
{
    public interface IReadModeRepository<T> where T : ReadMode
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Save(T t);
    }
}