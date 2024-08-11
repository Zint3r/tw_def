using System;
using System.Collections.Generic;

namespace Core.MessageHub
{
    public class MessageHubService : IMessageHubService
    {
        private readonly IMessageHubMainChannel _mainChannel;
        private Dictionary<int, IMessageChannel> _registeredChannels = new Dictionary<int, IMessageChannel>();
        private int _lastUsedId = -1;
        
        public MessageHubService()
        {
            _mainChannel = new DefaultChannel();
            RegisterChannel(_mainChannel);
        }
        
        public void Publish<T>(T message) where T : IMessage
        {
            PublishOnAllChannels(message);
        }
        
        public void Publish<T>(T message, IMessageChannel channel) where T : IMessage
        {
            channel.Publish(message);
        }
        
        private void PublishOnAllChannels<T>(T message) where T : IMessage
        {
            foreach (var channel in _registeredChannels.Values)
            {
                channel.Publish(message);
            }
        }

        public void Subscribe<T>(Action<T> callback, object target = null) where T : IMessage
        {
            Subscribe(callback, _mainChannel, target);
        }

        public void Subscribe<T>(Action<T> callback, IMessageChannel channel, object target = null) where T : IMessage
        {
            channel.Subscribe(callback, target);
        }
        
        public void Unsubscribe<T>(Action<T> callback, object subscriber = null)
        {
            Unsubscribe(callback, _mainChannel, subscriber);
        }

        public void Unsubscribe<T>(Action<T> callback, IMessageChannel channel, object subscriber = null)
        {
            channel.Unsubscribe(callback, subscriber);
        }
        
        public void UnsubscribeAllFromOneChannel(object subscriber, IMessageChannel channel)
        {
            channel.UnsubscribeAll(subscriber);
        }
        
        public void UnsubscribeAll(object subscriber)
        {
            foreach (var channel in _registeredChannels.Values)
            {
                channel.UnsubscribeAll(subscriber);
            }
        }
        
        public int RegisterChannel(IMessageChannel newChannel)
        {
            var uniqueId = GetNextUniqueId();
            _registeredChannels.Add(uniqueId, newChannel);
            return uniqueId;
        }
        
        public int GetNextUniqueId()
        {
            _lastUsedId++;
            return _lastUsedId;
        }
    }
}