using System;
using Microsoft.Owin.Hosting;

namespace DDD.Owin
{
    public class HttpCommunicator : ICommunicator
    {
        private IDisposable WebAppDisposable { get; set; }
        public HttpCommunicator(int port)
        {
            Port = port;
            Type = Communicate.Http;
        }
        public Communicate Type { get; set; }
        public int Port { get; set; }
        public string GetEndPoint()
        {
            return $"http://+:{Port}";
        }

        public void Start()
        {
            var url = GetEndPoint();
            WebAppDisposable = WebApp.Start<Startup>(url);
            Console.WriteLine($"Srevice start at:{url}");
        }
        public void Dispose()
        {
            WebAppDisposable.Dispose();
        }
    }
}