using System.Collections.Generic;

namespace Game.Features.Heroes
{
	public interface IHeroCollectionService
	{
		void InitializeCategories(List<HeroCategoryVO> categories);
		void InitializeHeroes(List<HeroVO> heroes);
		void InitializeRaces(List<HeroRaceVO> races);
		void InitializeClasses(Dictionary<string, List<HeroClassVO>> classes);
		void UpgradeHero(HeroVO hero);
	}
}