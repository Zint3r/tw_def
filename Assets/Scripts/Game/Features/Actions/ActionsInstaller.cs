using Zenject;

namespace Game.Features.Actions
{
    public class ActionsInstaller : Installer<ActionsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ActionLocator>().AsSingle();
        }
    }
}