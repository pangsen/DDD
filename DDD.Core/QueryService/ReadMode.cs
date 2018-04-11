using System;

namespace DDD.Core.QueryService
{
    public abstract class ReadMode
    {
        public Guid Id { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReadModelEventHandlerRegisterAttribute : Attribute
    {
        public Type ReadModelUpdaterType { get; set; }

        public ReadModelEventHandlerRegisterAttribute(Type type)
        {
            ReadModelUpdaterType = type;
        }
    }
}
