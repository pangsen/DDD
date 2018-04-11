using DDD.Core.Message;

namespace DDD.Domain
{
    public class EditToDoCommand : Command
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}