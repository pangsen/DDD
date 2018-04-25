using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;

namespace DDD.Owin
{
    public static class AsyncTcpSessionExtension
    {
        public static void Send(this AsyncTcpSession session, string message)
        {

            var messageBytes = Encoding.UTF8.GetBytes(message.EndsWith("\r\n") ? message : message + "\r\n");
            session.Send(messageBytes, 0, messageBytes.Length);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {



            ServiceContainer.AddAndStartService(new ServiceInfo { Name = "DemoService", Factory = () => new DemoService() });
            //            ServiceContainer.AddAndStartService(new ServiceInfo { Name = "DemoService2",Factory = () => new DemoService2() });
            ServiceProxy.CreateService<IDemoService>().Test(new TestCommand { Name = "sam", Password = "Password1" });
            Console.ReadLine();
            return;

            if (Console.ReadKey().KeyChar == 'd')
            {
                var serviceName = "DemoService";
                var actionName = "Test";
                var parameters = JsonConvert.SerializeObject(new TestCommand
                {
                    Name = "test",
                    Password = "test password"
                });
                var message = $"{serviceName}/{actionName}/{parameters}";
                var session = new AsyncTcpSession();
                var service = ServiceContainer.Services.First(a => a.Name == serviceName).GetInstance();
                session.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), service.Communicator.Port));
                session.Connected += Connected;
                session.Error += Error;
                session.DataReceived += DataReceived;
                session.Closed += Closed;
                while (!session.IsConnected)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(10)).GetAwaiter().GetResult();
                }
                session.Send(message);
                //                    .ForEach(service =>
                //                {
                //
                //                    session.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), service.GetInstance().Communicator.Port));
                //                    session.Connected += Connected;
                //                    session.Error += Error;
                //                    session.DataReceived += DataReceived;
                //                    session.Closed += Closed;
                //                });
                //                var client = new EasyClient();
                //                client.Initialize(new Test(), req=>
                //                {
                //                    Console.WriteLine(req.Body);
                //                });
                //                ServiceContainer.Services.ForEach(service =>
                //                {
                //                  
                //                    if (client.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), service.GetInstance().Communicator.Port)).Result)
                //                    {
                //                        client.Send(Encoding.UTF8.GetBytes("cccccccccccccccc\r\n"));
                //                    }
                //                });
                Console.ReadLine();
            }
        }

        private static void Closed(object sender, EventArgs e)
        {
            ((AsyncTcpSession)sender).Close();
        }

        private static void DataReceived(object sender, DataEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Data, 0, e.Length);
            Console.WriteLine(message);
            ((AsyncTcpSession)sender).Close();
        }

        private static void Error(object sender, ErrorEventArgs e)
        {
            ((AsyncTcpSession)sender).Close();
        }

        private static void Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Connect success.");
        }

        public class Test : TerminatorReceiveFilter<StringPackageInfo>
        {
            public Test() : base(Encoding.ASCII.GetBytes("||"))
            {
            }

            public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
            {
                var str = bufferStream.ReadString((int)bufferStream.Length, Encoding.UTF8);
                return new StringPackageInfo("", str, null);
            }
        }
    }
}
