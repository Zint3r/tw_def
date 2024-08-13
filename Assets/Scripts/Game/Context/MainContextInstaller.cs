using Game.Features;
using Game.Features.Actions;
using Game.Features.Level.Factories;
using Game.GameState;
using Game.UI.Loading;
using Game.Utils;
using Module.GameObjectInstaller;
using Module.Network.Installer;
using Module.UI;
using UnityEngine;
using Zenject;

namespace Game.Context
{
	public class MainContextInstaller : MonoInstaller<MainContextInstaller>
	{
		[SerializeField]
		private Camera previewCamera;

		[SerializeField]
		private Camera mapCamera;

		public override void InstallBindings()
		{
			Container.Bind<InitialLoadingScreenPresenter>().FromComponentInHierarchy().AsSingle();

			Container.Bind<IInitializable>().To<DPL>().AsSingle();

			Container.Bind<Camera>().WithId("GUICamera").FromInstance(previewCamera);
			Container.Bind<Camera>().WithId("MapCamera").FromInstance(mapCamera);

			NetworkGatewayInstaller.Install(Container);

			ActionsInstaller.Install(Container);

			UiServiceInstaller.Install(Container);

						

			GameStateInstaller.Install(Container);

			GameObjectInstaller.Install(Container);

			FactoriesInstaller.Install(Container);

			FeaturesInstaller.Install(Container);
		}
	}
}