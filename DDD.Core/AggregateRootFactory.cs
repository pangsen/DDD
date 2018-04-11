using System;
using System.Reflection;

namespace DDD.Core
{
    public class AggregateRootFactory
    {
        public static T Build<T>(Guid id) where T : AggregateRoot
        {
            var type = typeof(T);
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { }, null);
            if (constructor == null)
            {
                throw new Exception("No default constructor.");
            }

            var t = constructor.Invoke(null) as T;
            type.BaseType?.GetMethod("SetAggregateRootId", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(t, new object[] { id });
            return t;
        }
    }
}