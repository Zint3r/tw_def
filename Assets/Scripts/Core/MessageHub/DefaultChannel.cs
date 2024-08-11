using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.MessageHub
{
    public class DefaultChannel : Channel, IMessageHubMainChannel
    {
        private readonly Dictionary<Type, Dictionary<object, IList>> _registeredSubscriberActions =
            new Dictionary<Type, Dictionary<object, IList>>();
        
        private int _muteCount;
        
        public override void Subscribe<T>(Action<T> callback, object subscriber = null)
        {
            var resolvedSubscriber = ResolveSubscriber(callback, subscriber);

            var callbackType = typeof(T);

            Dictionary<object, IList> subscriptions;
            if (!_registeredSubscriberActions.TryGetValue(callbackType, out subscriptions))
            {
                subscriptions = new Dictionary<object, IList>();
                _registeredSubscriberActions.Add(callbackType, subscriptions);
            }

            IList delegates;
            if (!subscriptions.TryGetValue(resolvedSubscriber, out delegates))
            {
                delegates = new List<Action<T>>(new[] {callback});
                subscriptions.Add(resolvedSubscriber, delegates);
            }
            else if (!delegates.Contains(callback))
            {
                delegates.Add(callback);
            }
        }
        
        private object ResolveSubscriber<T>(Action<T> callback, object subscriber)
        {
            if (subscriber == null)
            {
                if (callback.Target != null)
                {
                    subscriber = callback.Target;
                }
                else
                {
                    throw new NotSupportedException(
                        "Subscribe static functions to a message is not supported!");
                }
            }

            ThrowWhenObjectIsCompilerGenerated(subscriber);
            return subscriber;
        }
        
        private void ThrowWhenObjectIsCompilerGenerated(object objectToTest)
        {
            foreach (var attribute in Attribute.GetCustomAttributes(objectToTest.GetType()))
            {
                if (attribute is CompilerGeneratedAttribute)
                {
                    throw new NotSupportedException(
                        "Subscribing/Unsubscribe with a compiler generated subscriber is not supported. " +
                        "You probably see this error because the given callback is a lambda or local function. " +
                        "Please define a subscriber object explicitly in this case.");
                }
            }
        }
        
        public override void UnsubscribeAll(object subscriber)
        {
            foreach (var subscriptions in _registeredSubscriberActions.Values)
            {
                if (subscriptions.ContainsKey(subscriber))
                {
                    subscriptions.Remove(subscriber);
                }
            }
        }
        
        public override void Unsubscribe<T>(Action<T> callback, object subscriber = null)
        {
            var resolvedSubscriber = ResolveSubscriber(callback, subscriber);

            var eventType = typeof(T);

            Dictionary<object, IList> subscriptions;
            IList subscriptionsPerObject;

            if (!_registeredSubscriberActions.TryGetValue(eventType, out subscriptions) ||
                !subscriptions.TryGetValue(resolvedSubscriber, out subscriptionsPerObject))
            {
                Debug.Log(string.Format("{0}  was already unsubscribed for target {1} ", typeof(T), resolvedSubscriber));
                return;
            }

            subscriptionsPerObject.Remove(callback);

            if (subscriptionsPerObject.Count == 0)
            {
                subscriptions.Remove(resolvedSubscriber);
            }

            if (subscriptions.Count == 0)
            {
                _registeredSubscriberActions.Remove(eventType);
            }
        }
        
        public override void Publish<T>(T message)
        {
            if (_muteCount > 0)
            {
                return;
            }

            List<Exception> caughtExceptions = new List<Exception>();
            Dictionary<object, IList> eventSubscriptions;
            if (_registeredSubscriberActions.TryGetValue(typeof(T), out eventSubscriptions))
            {
                var subscriptionCopy = new IList[eventSubscriptions.Count];
                eventSubscriptions.Values.CopyTo(subscriptionCopy, 0);

                foreach (var objectSubscriptions in subscriptionCopy)
                {
                    var castedList = ((List<Action<T>>) objectSubscriptions).ToArray();
                    foreach (var subscription in castedList)
                    {
                        InvokeCallbackOrLogException(() => subscription(message), ref caughtExceptions);
                    }
                }
            }

            foreach (var exception in caughtExceptions)
            {
                Debug.LogError($"{exception}:\n{message}");
            }
        }
        
        public void InvokeCallbackOrLogException(Action doAction, ref List<Exception> exceptions)
        {
            try
            {
                doAction();
            }
            catch (Exception e)
            {
                if (exceptions != null)
                    exceptions.Add(e);
            }
        }
    }
}