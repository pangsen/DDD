# DDD
DDD,CQRS,ES Demo project

>基本用法如下

<pre>
<code>


var builderOption = BuilderOption
                .New()
                .RegisterDefault(typeof(UserCommandHandle).Assembly);

var resolver = builderOption.ServiceRegistration.CreateResolver();

//Command
var personId = Guid.NewGuid();
var bus = resolver.Resolve&lt;IBus>();
bus.Send(new UserRegisterCommand { AggregateId = personId, UserName = "Sam Pang", Password = "Password" });
bus.Send(new CreateToDoCommand { AggregateId = Guid.NewGuid(), Title = "Todo Title", Description = "Todo Description", UserId = personId });
//Query
var userQueryService = resolver.Resolve&lt;QueryService&lt;UserReadModel>>();
var todoQueryService = resolver.Resolve&lt;QueryService&lt;ToDoReadModel>>();
var persons = userQueryService.GetAll();

persons.ForEach(a =>
{
    Console.WriteLine($"{a.Id},{a.UserName},{a.Password}");
    Console.WriteLine($"Dodo list count:{a.MyTodoList.Count}");
    a.MyTodoList.ForEach(t =>
    {
        var todo = todoQueryService.GetById(t);
        Console.WriteLine($"{todo.Id},{todo.Title},{todo.Description}");
    });
});
</code>
</pre>
