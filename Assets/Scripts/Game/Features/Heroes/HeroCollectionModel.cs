using Game.Features.GameDesign.DefinitionObjects.Heroes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Features.Heroes
{
	public interface IHeroCollectionDataProvider
	{
		void AddHeroOnScene(HeroStatsInfo heroStatsInfo);
		List<HeroStatsInfo> GetAllHeroOnScene();
		List<HeroCategoryVO> GetHeroCategories();
		List<HeroVO> GetHeroes();
		List<HeroRaceVO> GetHeroRaces();
		List<HeroVO> GetHeroesByCategory(HeroCategoryVO category);
		List<HeroClassVO> GetClassesByRace(HeroRaceVO raceVO);
		HeroVO GetHero(HeroClassDefinition definition);
	}
	public class HeroStatsInfo
	{
		public Transform HeroTransform;
		public HeroPresenter HeroPresenter;
		public HeroVO HeroVO = HeroVO.Empty;
		public float CurrentTimersToAttack = 0f;
	}
	public class HeroCollectionModel : IHeroCollectionDataProvider
	{
		public List<HeroCategoryVO> Categories = new List<HeroCategoryVO>();
		public List<HeroVO> Heroes = new List<HeroVO>();
		public List<HeroRaceVO> Races = new List<HeroRaceVO>();
		public Dictionary<string, List<HeroClassVO>> Classes = new Dictionary<string, List<HeroClassVO>>();

		private List<HeroStatsInfo> HeroesOnScene = new List<HeroStatsInfo>();
		public int CurrentUniqueHeroID = 1;

		public HeroVO GetHero(HeroClassDefinition definition)
		{
			return Heroes.FirstOrDefault(x => x.HeroDefinition.Equals(definition));
		}
		public List<HeroCategoryVO> GetHeroCategories()
		{
			return Categories;
		}
		public List<HeroRaceVO> GetHeroRaces()
		{
			return Races;
		}
		public List<HeroVO> GetHeroes()
		{
			return Heroes;
		}
		public List<HeroVO> GetHeroesByCategory(HeroCategoryVO category)
		{
			return Heroes.Where(x => x.CategoryDefinition.Equals(category.Definition)).ToList();
		}
		public List<HeroClassVO> GetClassesByRace(HeroRaceVO raceVO)
		{
			if (Classes.TryGetValue(raceVO.HeroRaceDefinition.AssetId, out List<HeroClassVO> classVO))
			{
				return classVO;
			}
			return null;
		}
		public List<HeroStatsInfo> GetAllHeroOnScene()
		{
			return HeroesOnScene;
		}
		public void AddHeroOnScene(HeroStatsInfo heroStatsInfo)
		{
			HeroesOnScene.Add(heroStatsInfo);
		}
	}
}