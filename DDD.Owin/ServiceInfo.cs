using System;

namespace DDD.Owin
{
    public class ServiceInfo
    {
        public string Name { get; set; }

         public Func<IService> Factory { get; set; }

        private IService Instance { get; set; }

        public IService GetInstance()
        {
            return Instance ?? (Instance = Factory());
        }


    }
}