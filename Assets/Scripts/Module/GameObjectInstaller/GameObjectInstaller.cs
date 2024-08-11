using Game.UI.Factories;
using Module.GameObjectInstaller.Pool;
using Zenject;

namespace Module.GameObjectInstaller
{
	public class GameObjectInstaller : Installer<GameObjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameObjectFactory>().AsSingle();
			Container.Bind<GoPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		}
    }
}