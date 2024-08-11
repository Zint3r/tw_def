using Zenject;

namespace Game.Features.MovePoints
{
	public class MovePointsInstaller : Installer<MovePointsInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<MovePointsService>().AsSingle();
			Container.BindInterfacesAndSelfTo<MovePointsModel>().AsSingle();
		}
	}
}