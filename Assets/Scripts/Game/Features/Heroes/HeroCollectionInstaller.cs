using Zenject;

namespace Game.Features.Heroes
{
	public class HeroCollectionInstaller : Installer<HeroCollectionInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<HeroCollectionService>().AsSingle();
			Container.BindInterfacesAndSelfTo<HeroCollectionModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<HeroesAutoAttackService>().AsSingle();
		}
	}
}