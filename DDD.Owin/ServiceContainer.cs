using System.Collections.Generic;

namespace DDD.Owin
{
    public class ServiceContainer
    {
        public static List<ServiceInfo> Services = new List<ServiceInfo>();

        public static void AddAndStartService(ServiceInfo serviceInfo)
        {
            serviceInfo.GetInstance().Communicator.Start();
            Services.Add(serviceInfo);
        }

    }
}