namespace DDD.Core.Bus
{
    public interface IEventHandler<in T> where T : Message.Event
    {
        void Handle(T @event);
    }
}