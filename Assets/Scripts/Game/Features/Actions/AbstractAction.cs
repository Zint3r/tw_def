using System;
using Core;
using Core.MessageHub;
using Module.DateTimeProvider;
using Module.Network;
using UnityEngine;

namespace Game.Features.Actions
{
    public interface IActionParams
    {
    }
    
    public abstract class AbstractAction<TMessage, TParams> where TMessage : IMessage where TParams : IActionParams
    {
        protected IDateTimeProvider dateTimeProvider;
        protected INetworkGateway networkGateway;
        protected IMessageHubService messageHubService;
        
        private Func<TMessage, NetworkAction> networkActionMap;
        
        public void Init(INetworkGateway networkGateway, IMessageHubService messageHubService, IDateTimeProvider dateTimeProvider)
        {
            this.networkGateway = networkGateway;
            this.messageHubService = messageHubService;
            this.dateTimeProvider = dateTimeProvider;
        }
        
        public void SetNetworkAction(Func<TMessage, NetworkAction> networkActionMapping)
        {
            networkActionMap = networkActionMapping;
        }
        
        public void Execute(TParams actionParams)
        {
            DateTime timeStamp = dateTimeProvider.UtcNow;
            if (!CanExecute(actionParams, timeStamp, out string errorMessage))
            {
                Debug.LogError($"Error during execution of {GetType().Name}: {errorMessage}");
                return;
            }
            
            UpdateModel(actionParams, timeStamp);
            SendNetworkRequest(CreateNetworkRequest(actionParams));
        }
        
        public virtual bool CanExecute(TParams actionParams, DateTime timeStamp, out string errorMessage)
        {
            errorMessage = ActionErrors.NoError;
            return true;
        }
        
        protected abstract TMessage CreateNetworkRequest(TParams actionParams);
        
        protected abstract void UpdateModel(TParams actionParams, DateTime timeStamp);
        
        private void SendNetworkRequest(TMessage networkAction)
        {
            networkGateway.Request(networkActionMap(networkAction));
        }
        
    }
}