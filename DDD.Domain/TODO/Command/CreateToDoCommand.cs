using System;
using DDD.Core.Message;

namespace DDD.Domain
{
    public class CreateToDoCommand : Command
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}