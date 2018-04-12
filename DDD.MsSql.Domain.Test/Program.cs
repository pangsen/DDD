using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Builder;
using DDD.Core;
using DDD.Core.Bus;
using DDD.IOC.StructureMap;
using DDD.MsSql.Domain.DbContext;
using DDD.MsSql.Domain.TODO.Command;
using DDD.MsSql.Domain.TODO.ReadModel;
using DDD.MsSql.Domain.User.Command;
using DDD.MsSql.Domain.User.ReadModel;

namespace DDD.MsSql.Domain.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContextProvider = new DbContextProvider();
            var builderOption = BuilderOptions
                .New()
                .UseStructureMapRegistration()
                .UseDefaultConfig()
                .UseMsSqlPersistent(a => dbContextProvider)
                .Register(typeof(UserRegisterCommand).Assembly);

            var resolver = builderOption.ServiceRegistration.CreateResolver();

            //Command

            var userQueryService = resolver.Resolve<QueryService<UserReadModel>>();
            //            var todoQueryService = resolver.Resolve<QueryService<ToDoReadModel>>();
            var bus = resolver.Resolve<IBus>();
            foreach (var i in Enumerable.Range(1, 1000))
            {
                Console.WriteLine(i);
                var personId = Guid.NewGuid();
                bus.Send(new UserRegisterCommand { AggregateId = personId, UserName = $"Sam Pang{i}", Password = $"Password{i}" });
                //                bus.Send(new CreateToDoCommand { AggregateId = Guid.NewGuid(), Title = $"Todo Title{i}", Description = $"Todo Description{i}", UserId = personId });
            }

            //Query
            var persons = userQueryService.GetAll();

            persons.ForEach(a =>
            {
                Console.WriteLine($"{a.Id},{a.UserName},{a.Password}");
            });
        }
    }
}

