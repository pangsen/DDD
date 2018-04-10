using System;
using DDD.Core.Message;

namespace DDD.Core.Bus
{
    public interface IBus
    {
        void Send<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Message.Event;
    }
}