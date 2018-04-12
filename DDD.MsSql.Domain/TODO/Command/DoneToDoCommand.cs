using System;

namespace DDD.MsSql.Domain.TODO.Command
{
    public class DoneToDoCommand : Core.Message.Command
    {
        public Guid TodoId { get; set; }

    }
}