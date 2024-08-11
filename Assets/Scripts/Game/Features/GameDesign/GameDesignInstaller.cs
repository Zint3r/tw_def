using Game.Features.GameDesign.Parsers;
using Zenject;

namespace Game.Features.GameDesign
{
	public class GameDesignInstaller : Installer<GameDesignInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameDefinitionModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LocalizationDefinitionParser>().AsSingle();
			Container.BindInterfacesAndSelfTo<HeroCollectionDefinitionParser>().AsSingle();
			Container.BindInterfacesAndSelfTo<ResourceDefinitionParser>().AsSingle();
			Container.BindInterfacesAndSelfTo<MapDefinitionParser>().AsSingle();
			Container.BindInterfacesAndSelfTo<MovePointsDefinitionParser>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyDefinitionParser>().AsSingle();
		}
    }
}