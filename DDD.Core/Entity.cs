using System;
using System.Collections.Generic;

namespace DDD.Core
{
    public abstract class Entity : ValueObject
    {
        protected Entity(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public Guid Id { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}