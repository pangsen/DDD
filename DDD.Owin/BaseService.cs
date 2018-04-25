using System;
using Microsoft.Owin.Hosting;

namespace DDD.Owin
{
    public class CommunicatorBuilder
    {
        public static ICommunicator Build()
        {
            return new HttpCommunicator(PortHelper.GetFirstAvailablePort());
        }
    }
    public class BaseService : IService
    {
        public ICommunicator Communicator { get; set; }

        public BaseService()
        {
            Communicator = CommunicatorBuilder.Build();
        }

        public void Start()
        {
            Communicator.Start();
        }
        ~BaseService()
        {
            Communicator.Dispose();
        }
    }
}