using System;
using System.Collections.Generic;
using DDD.Core.QueryService;
using DDD.MsSql.Domain.User.Event;

namespace DDD.MsSql.Domain.User.ReadModel
{
    public class UserReadModel : ReadMode
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Guid> MyTodoList { get; set; }=new List<Guid>();
        public void Apply(NewToDoAddedEvent @event)
        {
            MyTodoList.Add(@event.ToDoId);
        }
        public void Apply(UserRegistedEvent @event)
        {
            Id = @event.AggregateId;
            UserName = @event.UserName;
            Password = @event.Password;

        }
    }
}