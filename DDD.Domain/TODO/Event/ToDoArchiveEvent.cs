using DDD.Core;
using DDD.Core.QueryService;

namespace DDD.Domain
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class ToDoArchiveEvent : ToDoStatusChangedEvent
    {
        public ToDoArchiveEvent()
        {
            Status = new ToDoStatus { Status = Domain.Status.Archive };
        }
    }
}