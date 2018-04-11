using DDD.Core.Message;

namespace DDD.Domain.User
{
    public class UserRegisterCommand : Command
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}