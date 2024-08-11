using Zenject;

namespace Game.Features.Localization
{
	public class LocalizationInstaller : Installer<LocalizationInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<LocalizationService>().AsSingle();
			Container.BindInterfacesAndSelfTo<LocalizationModel>().AsSingle();
		}
	}
}
