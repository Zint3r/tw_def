using Zenject;

namespace Game.Features.Enemy
{
	public class EnemyesInstaller : Installer<EnemyesInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<EnemySpawnerService>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyReciveDamageService>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyMoveService>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyesModel>().AsSingle();
		}
	}
}