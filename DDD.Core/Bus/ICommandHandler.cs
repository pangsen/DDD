namespace DDD.Core.Bus
{
    public interface ICommandHandler<in T> where T : Message.Command
    {
        void Handle(T message);
    }
}