using System;
using System.Linq;
using DDD.Builder;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Domain;
using DDD.Domain.User;
using DDD.IOC.StructureMap;

namespace DDD.Mongo.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var builderOption = BuilderOptions
               .New()
               .UseStructureMapRegistration()
               .UseDefaultConfig()
               .UseMongoPersistent()
               .Register(typeof(UserCommandHandle).Assembly);

            var resolver = builderOption.ServiceRegistration.CreateResolver();

            //Command

            var userQueryService = resolver.Resolve<QueryService<UserReadModel>>();
            var todoQueryService = resolver.Resolve<QueryService<ToDoReadModel>>();

            foreach (var i in Enumerable.Range(1, 1000))
            {
                var personId = Guid.NewGuid();
                var bus = resolver.Resolve<IBus>();
                bus.Send(new UserRegisterCommand { AggregateId = personId, UserName = $"Sam Pang{i}", Password = $"Password{i}" });
                bus.Send(new CreateToDoCommand { AggregateId = Guid.NewGuid(), Title = $"Todo Title{i}", Description = $"Todo Description{i}", UserId = personId });
            }

            //Query
            var persons = userQueryService.GetAll();

            persons.ForEach(a =>
            {
                Console.WriteLine($"{a.Id},{a.UserName},{a.Password}");
                Console.WriteLine($"Dodo list count:{a.MyTodoList.Count}");
                a.MyTodoList.ForEach(t =>
                {
                    var todo = todoQueryService.GetById(t);
                    Console.WriteLine($"{todo.Id},{todo.Title},{todo.Description}");
                });
            });

            Console.ReadLine();
        }
    }
}
