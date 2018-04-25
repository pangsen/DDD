namespace DDD.Owin
{
    public interface IService
    {
        ICommunicator Communicator { get; set; }
    }
}