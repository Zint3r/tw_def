using Core.Map;
using Core.UiService;
using Game.UI.Factories;
using Zenject;

namespace Module.UI
{
    public class UiServiceInstaller : Installer<UiServiceInstaller>
    {
        public override void InstallBindings()
        {
            UiServiceSettings uiServiceSettings = new UiServiceSettings();
            Container.Bind<UiServiceSettings>().FromInstance(uiServiceSettings).AsSingle();
            
            Container.Bind<UiRoot>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<UiPool>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<IUiLoader>().To<UiLoaderAssetReferencePrefab>().AsSingle();
            
            //Container.Bind<MapRoot>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<UiService>().AsSingle().NonLazy();
            //Container.BindInterfacesTo<MapService>().AsSingle().NonLazy();
            
            Container.Bind<IconFactory>().AsSingle();
            Container.Bind<UiConfigurationFactory>().AsSingle();
        }
    }
}