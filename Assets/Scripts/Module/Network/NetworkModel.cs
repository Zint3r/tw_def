using Core.MessageHub;
using Zenject;

namespace Module.Network
{
    public class NetworkModel
    {
        [Inject]
        private IMessageHubService messageHubService;
        
        private bool connectedToServer;

        public bool ConnectedToServer
        {
            get => connectedToServer;
            set
            {
                if (value != connectedToServer)
                {
                    messageHubService.Publish(new NetworkMessages.ConnectionStatusChangedMessage(value));
                }
                connectedToServer = value;
            }
        }
    }
}