using Game.Features.Level.Presenters;
using Zenject;

namespace Game.Features.Level
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LevelPresenter>().AsSingle();
        }
    }
}