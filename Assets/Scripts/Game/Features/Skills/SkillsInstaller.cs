using Game.Features.Skills;
using Zenject;

namespace Game.Features.Heroes
{
	public class SkillsInstaller : Installer<SkillsInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ProjectilesService>().AsSingle();
			Container.BindInterfacesAndSelfTo<SkillsModel>().AsSingle();
		}
	}
}