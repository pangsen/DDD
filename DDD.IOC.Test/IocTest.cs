using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DDD.IOC.Test
{
    public class Repository<T, T1, T2> where T : class, T1 where T1 : class, T2 where T2 : ClassA
    {
        public Repository()
        {

        }

        public Repository(string str, int num)
        {

        }
    }

    public class ClassA
    {

    }

    public abstract class LogType
    {
        public string Name { get; set; }
    }

    public abstract class LogWriter
    {
        public virtual void Log(string message)
        {
            Log(message);
        }
    }

    public class ErrorLog : LogType
    {
        public ErrorLog()
        {
            Name = "Error";
        }
    }

    public class ConsoleWriter : LogWriter
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
    public interface ILogger<T, T1>
    {
        void Log(string message);
    }

    public class Logger<T, T1> : ILogger<T, T1> where T : LogType where T1 : LogWriter
    {
        private readonly T _logType;
        private readonly T1 _logWriter;

        public Logger(T logType, T1 logWriter)
        {
            _logType = logType;
            _logWriter = logWriter;
        }

        public void Log(string message)
        {
            Console.WriteLine($"{typeof(T).Name},{typeof(T1).Name},{message}");
            _logWriter.Log(_logType.Name + ":" + message);
        }
    }
    public interface IUser
    {

    }

    public class Customer : IUser
    {

    }

    public class Worker
    {
        private readonly IUser _user;

        public Worker(IUser user)
        {
            _user = user;
        }

        public void Work()
        {
            Console.WriteLine($"{_user.GetType().Name} start work.");
        }
    }
    public interface IAA { }
    public interface IA: IAA { }
    public class A : IA
    {
        
    }

    public class B : A
    {
        
    }
    public class IocTest
    {
        [Test]
        public void Test()
        {



            var type = typeof(B);
            Console.WriteLine(type.BaseType.Name);
            Console.WriteLine(type.BaseType.BaseType.Name);
            Console.WriteLine(string.Join(",",type.GetInterfaces().Select(a=>a.Name)));

            //            var type = typeof(Repository<,,>);
            //            //            var type = new Repository<string, bool, int>().GetType();
            //            TypeInfo typeInfo = type.GetTypeInfo();
            //            Write("typeInfo", typeInfo);
            //            Write("type.GetGenericTypeDefinition()", type.GetGenericTypeDefinition());
            //            Write("typeInfo.GenericTypeParameters", typeInfo.GenericTypeParameters);
            //            Write("typeInfo.GenericTypeArguments", typeInfo.GenericTypeArguments);
            //            Write("type.GetConstructors()", typeInfo.GetConstructors());
        }

        [Test]
        public void TestRegistration()
        {
            var xx = new DefaultServiceResitration();
            xx.Register<IUser, Customer>();
            xx.RegisterType(typeof(Worker));
            xx.RegisterType(typeof(Customer));
            xx.RegisterType(typeof(ErrorLog));
            xx.RegisterType(typeof(ConsoleWriter));
            xx.RegisterGeneric(typeof(ILogger<,>), typeof(Logger<,>));
            xx.Register<IUser>(context => context.Resolver.Resolve<Customer>());
            var resolver = xx.CreateResolver();
            var logger = resolver.Resolve<ILogger<ErrorLog, ConsoleWriter>>();
            logger.Log("xxxxxxxxxxxxxxx");
            var worker = resolver.Resolve<Worker>();
            worker.Work();
        }
        private void Write(string str, object obj)
        {
            Console.WriteLine(str + "===========>" + obj.ToString());
            var typeInfo = obj.GetType();
            if (typeInfo.IsArray ||
                (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                Console.WriteLine("------------------start---------------------");
                var xx = ((IEnumerable)obj).GetEnumerator();
                while (xx.MoveNext())
                {
                    Write("", xx.Current);
                }

            }
            Console.WriteLine("------------------end---------------------");
        }
    }
}
