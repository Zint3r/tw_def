using System;

namespace Core.MessageHub
{
    public interface IMessageChannel
    {
        void Subscribe<T>(Action<T> callback, object subscriber = null) where T : IMessage;
        void Unsubscribe<T>(Action<T> callback, object subscriber = null);
        void UnsubscribeAll(object subscriber);
        void Publish<T>(T message) where T : IMessage;
    }
}