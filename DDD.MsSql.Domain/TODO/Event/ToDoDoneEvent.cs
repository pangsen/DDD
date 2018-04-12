using DDD.Core;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.TODO.ReadModel;

namespace DDD.MsSql.Domain.TODO.Event
{
    [ReadModelEventHandlerRegister(typeof(ReadModelEventHandler<ToDoReadModel>))]
    public class ToDoDoneEvent : ToDoStatusChangedEvent
    {
        public ToDoDoneEvent()
        {
            Status = new ToDoStatus { Status = TODO.Status.Done };
        }

    }
}