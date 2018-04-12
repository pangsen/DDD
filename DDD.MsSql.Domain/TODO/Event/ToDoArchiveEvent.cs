using DDD.Core;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.TODO.ReadModel;

namespace DDD.MsSql.Domain.TODO.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class ToDoArchiveEvent : ToDoStatusChangedEvent
    {
        public ToDoArchiveEvent()
        {
            Status = new ToDoStatus { Status = TODO.Status.Archive };
        }
    }
}