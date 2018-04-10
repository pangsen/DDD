using DDD.Core.Message;

namespace DDD.Domain.CommandHandler
{
    public class PersonChangeNameCommand : Command
    {
        public string Name { get; set; }
    }
}