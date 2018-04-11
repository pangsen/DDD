using System;
using DDD.Core.Message;

namespace DDD.Domain
{
    public class ArchiveToDoCommand : Command
    {
        public Guid TodoId { get; set; }

    }
}