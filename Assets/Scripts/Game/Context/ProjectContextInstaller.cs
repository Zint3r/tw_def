using Core.CoroutineProvider;
using Game.Application;
using Module.DateTimeProvider;
using Module.MessageHub;
using Zenject;

namespace Game.Context
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            //Container.Bind<EventSystem>().FromComponentInNewPrefab(EventSystemPrefab).AsSingle().NonLazy();
            Container.BindInterfacesTo<ApplicationService>().AsSingle();
            
            Container.Bind<ICoroutineService>().To<CoroutineService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<IDateTimeProvider>().To<DateTimeProvider>().AsSingle();

            MessageHubInstaller.Install(Container);
        }
    }
}