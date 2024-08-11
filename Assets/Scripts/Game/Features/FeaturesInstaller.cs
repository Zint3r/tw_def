using Core.UiService;
using Game.Features.Enemy;
using Game.Features.GameDesign;
using Game.Features.Heroes;
using Game.Features.Localization;
using Game.Features.MapCreator;
using Game.Features.MovePoints;
using Game.Features.PlayerControl;
using Game.Features.Resource;
using Zenject;

namespace Game.Features
{
	public class FeaturesInstaller : Installer<FeaturesInstaller>
	{
		public override void InstallBindings()
		{
			GameDesignInstaller.Install(Container);			
			LocalizationInstaller.Install(Container);
			ResourceInstaller.Install(Container);
			HeroCollectionInstaller.Install(Container);
			MapCreatorInstaller.Install(Container);
			MovePointsInstaller.Install(Container);
			EnemyesInstaller.Install(Container);
			Container.Bind<PlayerControlInput>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		}
	}
}