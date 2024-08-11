using System;

namespace Core.MessageHub
{
    public abstract class Channel : IMessageChannel
    {
        public int Id { get; protected set; }
        
        public abstract void Subscribe<T>(Action<T> callback, object subscriber = null) where T : IMessage;
        
        public abstract void Unsubscribe<T>(Action<T> callback, object subscriber = null);
        
        public abstract void UnsubscribeAll(object subscriber);
        
        public abstract void Publish<T>(T message) where T : IMessage;
    }
}