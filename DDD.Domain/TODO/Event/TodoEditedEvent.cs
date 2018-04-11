using DDD.Core;
using DDD.Core.QueryService;

namespace DDD.Domain
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class TodoEditedEvent : Core.Message.Event
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}