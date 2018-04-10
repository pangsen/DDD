using System;

namespace DDD.Core.QueryService
{
    public abstract class ReadMode
    {
        public Guid Id { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReadModelUpdaterAttribute : Attribute
    {
        public Type ReadModelUpdaterType { get; set; }

        public ReadModelUpdaterAttribute(Type type)
        {
            ReadModelUpdaterType = type;
        }
    }
}
