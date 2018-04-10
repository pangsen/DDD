# DDD
DDD,CQRS,ES Demo project

>基本用法如下

<pre>
<code>
var builderOption = BuilderOption
    .New()
    .RegisterDefault(typeof(PersonCreateCommand).Assembly);

var resolver = builderOption.ServiceRegistration.CreateResolver();

//Command
var personId = Guid.NewGuid();
var bus = resolver.Resolve<IBus>();
bus.Send(new PersonCreateCommand { AggregateId = personId, Name = "Sam Pang" });
bus.Send(new PersonChangeNameCommand { AggregateId = personId, Name = "Pang Sen" });

//Query
var personQueryService = resolver.Resolve<QueryService<PersonReadMode>>();
var persons = personQueryService.GetAll();
Console.WriteLine($"Person count:{persons.Count}");

persons.ForEach(a =>
{
    Console.WriteLine($"{a.Id},{a.Name}");
});
</code>
</pre>
