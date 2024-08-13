using Game.Features.GameDesign.DefinitionObjects.Heroes;
using Game.Features.Heroes;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class HeroCollectionDefinitionParser
    {
        [Inject]
        private GameDefinitionModel gameDefinitionModel;
        
        [Inject] 
        private IHeroCollectionService heroCollectionService;
        
        public IEnumerator Initialize()
        {            
            List<HeroCategoryVO> categories = new List<HeroCategoryVO>
            {
                new HeroCategoryVO
                {
                    Name = "Fighter",
                    Definition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
                },
                new HeroCategoryVO
                {
                    Name = "Ranger",
                    Definition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
                }
            };            
            heroCollectionService.InitializeCategories(categories);

			List<HeroRaceVO> races = new List<HeroRaceVO>
			{
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace)
				},
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace)
				},
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace)
				},
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace)
				},
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace)
				},
				new HeroRaceVO
				{
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.KamaelRace)
				}
			};
			heroCollectionService.InitializeRaces(races);

			Dictionary<string, List<HeroClassVO>> classes = new Dictionary<string, List<HeroClassVO>>
			{
				{
					HeroCollectionConstants.HumanRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Knight)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Rogue)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Warrior)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Cleric)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Wizard)
						}
					}
				},
				{
					HeroCollectionConstants.ElfRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Knight)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Scout)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Oracle)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Wizard)
						}
					}
				},
				{
					HeroCollectionConstants.DarkelfRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Knight)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Assassin)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Oracle)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Wizard)
						}
					}
				},
				{
					HeroCollectionConstants.OrcRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Raider)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Monk)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Shaman)
						}
					}
				},
				{
					HeroCollectionConstants.DwarfRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Artisan)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Scavenger)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Wizard)
						}
					}
				},
				{
					HeroCollectionConstants.KamaelRace,
					new List<HeroClassVO>
					{
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.KamaelRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Kamael_Trooper)
						},
						new HeroClassVO
						{
							HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.KamaelRace),
							HeroClassDefinition = gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Kamael_Warder)
						}
					}
				}
			};
			heroCollectionService.InitializeClasses(classes);

			List<HeroVO> heroes = new List<HeroVO>
            {
                new HeroVO
                {
					Id = 1,
					LocalizationId = 1,
					MinDamage = 1,
					MaxDamage = 5,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 2,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Knight),					
                    CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
                },
                new HeroVO
                {
					Id = 2,
					LocalizationId = 2,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Rogue),
                    CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
                },
				new HeroVO
				{
					Id = 3,
					LocalizationId = 3,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Warrior),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 4,
					LocalizationId = 4,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Cleric),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 5,
					LocalizationId = 5,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.HumanRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Human_Wizard),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 6,
					LocalizationId = 6,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Knight),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 7,
					LocalizationId = 7,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Scout),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 8,
					LocalizationId = 8,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Oracle),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 9,
					LocalizationId = 9,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.ElfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Elven_Wizard),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 10,
					LocalizationId = 10,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Knight),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 11,
					LocalizationId = 11,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Assassin),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 12,
					LocalizationId = 12,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Oracle),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 13,
					LocalizationId = 13,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DarkelfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dark_Wizard),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 14,
					LocalizationId = 14,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Raider),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 15,
					LocalizationId = 15,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Monk),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 16,
					LocalizationId = 16,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.OrcRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Orc_Shaman),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 17,
					LocalizationId = 17,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Artisan),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 18,
					LocalizationId = 18,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Scavenger),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 19,
					LocalizationId = 19,
					MinDamage = 1,
					MaxDamage = 2,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.DwarfRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Dwarf_Wizard),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.RangerCategory)
				},
				new HeroVO
				{
					Id = 20,
					LocalizationId = 20,
					MinDamage = 2,
					MaxDamage = 4,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.KamaelRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Kamael_Trooper),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				},
				new HeroVO
				{
					Id = 21,
					LocalizationId = 21,
					MinDamage = 1,
					MaxDamage = 5,
					AttackSpeed = 1f,
					CritChance = 5f,
					CritMulti = 2f,
					Price = 10,
					HeroRaceDefinition = gameDefinitionModel.GetOrCreate<HeroRaceDefinition>(HeroCollectionConstants.KamaelRace),
					HeroDefinition =  gameDefinitionModel.GetOrCreate<HeroClassDefinition>(HeroCollectionConstants.Class_Kamael_Warder),
					CategoryDefinition = gameDefinitionModel.GetOrCreate<HeroCategoryDefinition>(HeroCollectionConstants.FighterCategory)
				}
			};
            heroCollectionService.InitializeHeroes(heroes);            
            yield break;
        }
    }
}