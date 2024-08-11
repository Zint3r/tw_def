using Game.Features.GameDesign.DefinitionObjects.Map;
using Game.Features.MapCreator;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class MapDefinitionParser
	{
        [Inject]
        private GameDefinitionModel gameDefinitionModel;
        
        [Inject] 
        private IMapCreatorService mapCreatorService;
        
        public IEnumerator Initialize()
        {
            List<MapVO> maps = new List<MapVO>
            {
                new MapVO
				{
					Name = MapConstants.Aden,
                    MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
                },
                new MapVO
				{
					Name = MapConstants.Dion,
                    MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
                },
				new MapVO
				{
					Name = MapConstants.Giran,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
				},
				new MapVO
				{
					Name = MapConstants.Goddard,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
				},
				new MapVO
				{
					Name = MapConstants.Heine,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
				},
				new MapVO
				{
					Name = MapConstants.Oren,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
				},
				new MapVO
				{
					Name = MapConstants.Rune,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
				},
				new MapVO
				{
					Name = MapConstants.Schuttgart,
					MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
				}
			};

			mapCreatorService.InitializeMaps(maps);            
            yield return null;
        }
    }
}