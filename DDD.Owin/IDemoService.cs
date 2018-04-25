namespace DDD.Owin
{
    public interface IDemoService : IService
    {
        string Test(TestCommand command);
    }
    public interface IDemoService2 : IService
    {
        TestCommand Test(TestCommand command);
    }
}