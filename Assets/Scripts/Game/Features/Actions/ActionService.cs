using System;
using Core;
using Core.MessageHub;
using Module.DateTimeProvider;
using Module.Network;
using Zenject;

namespace Game.Features.Actions
{
    //TODO: Maybe static class ? 
    public class ActionService
    {
        [Inject]
        protected INetworkGateway networkGateway;

        [Inject]
        protected IMessageHubService messageHubService;

        [Inject]
        protected IDateTimeProvider dateTimeProvider;

        [Inject]
        protected DiContainer diContainer;
        
        protected T RegisterAction<T, TMessage, TParams>(Func<TMessage, NetworkAction> networkActionMapping)
            where TMessage : IMessage
            where TParams : IActionParams
            where T : AbstractAction<TMessage, TParams>, new()
        {
            T action = diContainer.Instantiate<T>();
            action.Init(networkGateway, messageHubService, dateTimeProvider);
            action.SetNetworkAction(networkActionMapping);
            return action;
        }
    }
}