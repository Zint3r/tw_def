using Module.States;
using Zenject;

namespace Game.GameState
{
    public class GameStateInstaller : Installer<GameStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<GameStateChartModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateChart>().AsSingle();
        }
    }
}