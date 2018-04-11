using DDD.Core;
using DDD.Core.QueryService;

namespace DDD.Domain
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class ToDoDoneEvent : ToDoStatusChangedEvent
    {
        public ToDoDoneEvent()
        {
            Status = new ToDoStatus { Status = Domain.Status.Done };
        }

    }
}