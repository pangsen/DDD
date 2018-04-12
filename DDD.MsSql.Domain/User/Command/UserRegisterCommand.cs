namespace DDD.MsSql.Domain.User.Command
{
    public class UserRegisterCommand : Core.Message.Command
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}