using System;

namespace Core.MessageHub
{
    public interface IMessageHubService : IMessageChannel
    {
        void Publish<T>(T message, IMessageChannel channel) where T : IMessage;
        void Subscribe<T>(Action<T> callback, IMessageChannel channel, object subscriber = null) where T : IMessage;
        
        int RegisterChannel(IMessageChannel channel);
    }
}