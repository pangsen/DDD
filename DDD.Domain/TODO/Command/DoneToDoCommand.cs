using System;
using DDD.Core.Message;

namespace DDD.Domain
{
    public class DoneToDoCommand : Command
    {
        public Guid TodoId { get; set; }

    }
}