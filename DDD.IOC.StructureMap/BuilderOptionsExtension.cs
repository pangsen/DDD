using DDD.Builder;
using StructureMap;

namespace DDD.IOC.StructureMap
{
    public static class BuilderOptionsExtension
    {
        public static BuilderOptions UseStructureMapRegistration(this BuilderOptions options, Container container = null)
        {
            options.ServiceRegistration = new StructureMapRegistration(container);
            return options;
        }
    }
}