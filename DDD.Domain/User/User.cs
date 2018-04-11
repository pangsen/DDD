using System;
using System.Collections.Generic;
using DDD.Core;

namespace DDD.Domain.User
{
    public class User : AggregateRoot
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Guid> MyTodoList { get; set; }=new List<Guid>();

        public void NewTodoAdded(Guid todoId)
        {
            ApplyEvent(new NewToDoAddedEvent { AggregateId = AggregateRootId, ToDoId = todoId });
        }

        public void Registe(string userName, string password)
        {
            ApplyEvent(new UserRegistedEvent { AggregateId = AggregateRootId, UserName = userName, Password = password });
        }


        public void Apply(NewToDoAddedEvent @event)
        {
            MyTodoList.Add(@event.ToDoId);
        }
        public void Apply(UserRegistedEvent @event)
        {
            UserName = UserName;
            Password = Password;

        }
    }
}