using System;

namespace DDD.MsSql.Domain.TODO.Command
{
    public class CreateToDoCommand : Core.Message.Command
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}