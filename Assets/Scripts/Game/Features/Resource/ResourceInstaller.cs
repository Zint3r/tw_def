using Zenject;

namespace Game.Features.Resource
{
    public class ResourceInstaller : Installer<ResourceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResourceService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceModel>().AsSingle();
        }
    }
}