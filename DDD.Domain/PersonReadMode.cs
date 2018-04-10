using DDD.Core.QueryService;
using DDD.Domain.Event;

namespace DDD.Domain
{
    public class PersonReadMode : ReadMode
    {
        public string Name { get; set; }
        public void Apply(PersonCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
        }
        public void Apply(PersonNameEvent @event)
        {
            Name = @event.Name;
        }
    }

}
