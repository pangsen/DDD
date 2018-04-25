using System;

namespace DDD.Owin
{
    public class DemoService : BaseService, IDemoService
    {
        public string Test(TestCommand command)
        {
            Console.WriteLine($"{command.Name},{command.Password}");
            return "executed";
        }
    }

    public class DemoService2 : BaseService, IDemoService2
    {
        public TestCommand Test(TestCommand command)
        {
            return command;
        }
    }
}