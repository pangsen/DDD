using System;
using System.Linq;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Domain;
using DDD.Domain.User;

namespace DDD.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var builderOption = BuilderOption
                .New()
                .RegisterDefault(typeof(UserCommandHandle).Assembly);

            var resolver = builderOption.ServiceRegistration.CreateResolver();

            //Command
            var personId = Guid.NewGuid();
            var bus = resolver.Resolve<IBus>();
            bus.Send(new UserRegisterCommand { AggregateId = personId, UserName = "Sam Pang", Password = "Password" });
            bus.Send(new CreateToDoCommand { AggregateId = Guid.NewGuid(), Title = "Todo Title", Description = "Todo Description", UserId = personId });
            //Query
            var userQueryService = resolver.Resolve<QueryService<UserReadModel>>();
            var todoQueryService = resolver.Resolve<QueryService<ToDoReadModel>>();
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
