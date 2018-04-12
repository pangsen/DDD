using System;

namespace DDD.MsSql.Domain.TODO.Command
{
    public class ArchiveToDoCommand : Core.Message.Command
    {
        public Guid TodoId { get; set; }

    }
}