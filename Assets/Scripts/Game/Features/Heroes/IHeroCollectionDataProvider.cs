using Game.Features.GameDesign.DefinitionObjects.Heroes;
using System.Collections.Generic;

namespace Game.Features.Heroes
{
	public interface IHeroCollectionDataProvider
	{
		List<HeroCategoryVO> GetHeroCategories();
		List<HeroVO> GetHeroes();
		List<HeroRaceVO> GetHeroRaces();
		List<HeroVO> GetHeroesByCategory(HeroCategoryVO category);
		List<HeroClassVO> GetClassesByRace(HeroRaceVO raceVO);
		HeroVO GetHero(HeroClassDefinition definition);
	}
}