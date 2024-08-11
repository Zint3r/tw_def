using Core;
using Core.MessageHub;
using Zenject;

namespace Module.MessageHub
{
    public class MessageHubInstaller : Installer<MessageHubInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MessageHubService>().AsSingle();
        }
    }
}