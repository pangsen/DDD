using DDD.Core.Message;

namespace DDD.Domain.CommandHandler
{
    public class PersonCreateCommand : Command
    {
        public string Name { get; set; }
    }
}