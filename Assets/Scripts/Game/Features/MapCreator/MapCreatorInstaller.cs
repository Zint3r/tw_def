using Zenject;

namespace Game.Features.MapCreator
{
	public class MapCreatorInstaller : Installer<MapCreatorInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<MapCreatorService>().AsSingle();
			Container.BindInterfacesAndSelfTo<MapCreatorModel>().AsSingle();
		}
	}
}