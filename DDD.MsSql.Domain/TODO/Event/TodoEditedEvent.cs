using DDD.Core;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.TODO.ReadModel;

namespace DDD.MsSql.Domain.TODO.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class TodoEditedEvent : Core.Message.Event
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}