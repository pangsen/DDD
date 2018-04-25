using System;

namespace DDD.Owin
{
    public interface ICommunicator : IDisposable
    {
        Communicate Type { get; set; }
        int Port { get; set; }
        string GetEndPoint();
        void Start();
    }
}