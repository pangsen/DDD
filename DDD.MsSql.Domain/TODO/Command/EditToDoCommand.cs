namespace DDD.MsSql.Domain.TODO.Command
{
    public class EditToDoCommand : Core.Message.Command
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}