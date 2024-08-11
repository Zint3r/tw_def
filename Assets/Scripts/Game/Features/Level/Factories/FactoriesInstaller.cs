using Zenject;

namespace Game.Features.Level.Factories
{
    public class FactoriesInstaller  : Installer<FactoriesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelEnvironmentFactory>().AsSingle();
            Container.Bind<LevelViewConfigurationFactory>().AsSingle();
        }
    }
}