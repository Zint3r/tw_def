using Zenject;

namespace Module.Network.Installer
{
    public class NetworkGatewayInstaller : Installer<NetworkGatewayInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkModel>().AsSingle();
            Container.Bind<INetworkGateway>().To<NetworkGateway>().AsSingle();
        }
    }
}