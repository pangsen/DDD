using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace DDD.Owin
{
    public static class InvocationExtension
    {
        public static RequestedServiceInfo ToRequestedServiceInfo(this IInvocation invocation)
        {
            
            var serviceName = invocation.Method.DeclaringType?.Name.Substring(1);
            var actionName = invocation.Method.Name;
            var paramters = JsonConvert.SerializeObject(invocation.Arguments.Select(JsonConvert.SerializeObject));
            return new RequestedServiceInfo
            {
                Name = serviceName,
                Action = actionName,
                Paramaters = paramters
            };
        }
    }
    public class xx : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
           var serviceInfo= invocation.ToRequestedServiceInfo();
            new HttpClient().PostAsync($"http://localhost:5000/{serviceInfo.Name}/{serviceInfo.Action}", new StringContent(serviceInfo.Paramaters));
        }
    }
    public class ServiceProxy
    {
        public static T CreateService<T>() where T : IService
        {
            var generator = new ProxyGenerator();
            return (T)generator.CreateInterfaceProxyWithoutTarget(typeof(T), new xx());

        }
    }
    public class MyServer : AppServer<AppSession>
    {
        public MyServer()
            : base(new CommandLineReceiveFilterFactory(Encoding.UTF8, new BasicRequestInfoParser("!@#$%", "!@#$%")))
        {

        }
    }
    public class TcpCommunicator : ICommunicator
    {
        public TcpCommunicator(int port)
        {
            Port = port;
        }
        private MyServer AppServer { get; set; }
        public Communicate Type { get; set; }
        public int Port { get; set; }
        public string GetEndPoint()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            AppServer = new MyServer();

            var port = PortHelper.GetFirstAvailablePort();
            if (!AppServer.Setup(port))
            {
                throw new Exception("Failed to setup!");
            }
            if (!AppServer.Start())
            {
                throw new Exception("Failed to start!");
            }
            Console.WriteLine($"start at port:{port}");
            AppServer.NewRequestReceived += StringRequesthandler;
            AppServer.NewSessionConnected += NewConnectionCome;
        }

        private void NewConnectionCome(AppSession session)
        {

        }

        private void StringRequesthandler(AppSession session, StringRequestInfo requestinfo)
        {
            var args = requestinfo.Body.Split('/');
            var info = new RequestedServiceInfo
            {
                Name = args[0],
                Action = args[1],
                Paramaters = string.Join("/", args.Skip(2).Take(args.Length - 2))
            };
            var result = ServiceExecuter.Execute(info);
            Console.WriteLine();
            session.Send($"{JsonConvert.SerializeObject(result.Result)}\r\n");
        }

        public void Dispose()
        {
            AppServer.Stop();
        }
    }
}