using System.Collections.Generic;
using Unity.VisualScripting;
using Zenject;

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
	public class HeroCollectionService : IHeroCollectionService
	{
		[Inject]
		private HeroCollectionModel collectionModel;

		public void InitializeCategories(List<HeroCategoryVO> categories)
		{
			collectionModel.Categories.AddRange(categories);
		}
		public void InitializeHeroes(List<HeroVO> heroes)
		{
			collectionModel.Heroes.AddRange(heroes);
		}
		public void InitializeRaces(List<HeroRaceVO> races)
		{
			collectionModel.Races.AddRange(races);
		}
		public void InitializeClasses(Dictionary<string, List<HeroClassVO>> classes)
		{
			collectionModel.Classes.AddRange(classes);
		}
		public void UpgradeHero(HeroVO hero)
		{
			for (int i = collectionModel.Heroes.Count - 1; i >= 0; i--)
			{
				if (collectionModel.Heroes[i].HeroDefinition.Equals(hero.HeroDefinition))
				{
					collectionModel.Heroes.RemoveAt(i);
					break;
				}
			}
			//List<ResourceVO> changedResources = DPL.UpgradeProvider.GetUpgradesByHero(Hero.HeroDefinition.DefinitionId).FirstOrDefault(x => x.Level == Hero.HeroLevel).Resources;
			//resourceService.UpdateResources(changedResources);
			//Hero.HeroLevel++;
			collectionModel.Heroes.Add(hero);
			//messageHubService.Publish(new HeroUpgradeMessage.UpgradedMessage(Hero));
		}
	}
}