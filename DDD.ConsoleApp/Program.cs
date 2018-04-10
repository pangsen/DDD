using System;
using System.Linq;
using DDD.Core;
using DDD.Core.Bus;
using DDD.Domain;
using DDD.Domain.CommandHandler;

namespace DDD.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var builderOption = BuilderOption.New()
                .RegisterDefault(typeof(PersonCreateCommand).Assembly);
            var bus = builderOption.ServiceRegistration.CreateResolver().Resolve<IBus>();
            var personQueryService =
                builderOption.ServiceRegistration.CreateResolver().Resolve<QueryService<PersonReadMode>>();
            //Command
            var personId = Guid.NewGuid();
            bus.Send(new PersonCreateCommand { AggregateId = personId, Name = "Sam Pang" });
            bus.Send(new PersonChangeNameCommand { AggregateId = personId, Name = "Pang Sen" });

            //Query
            var persons = personQueryService.GetAll();
            Console.WriteLine($"Person count:{persons.Count}");
            persons.ForEach(a =>
            {
                Console.WriteLine($"{a.Id},{a.Name}");
            });
            Console.ReadLine();
        }
    }
}
