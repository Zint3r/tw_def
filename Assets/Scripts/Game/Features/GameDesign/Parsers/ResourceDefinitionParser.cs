using Game.Features.Resource;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class ResourceDefinitionParser
    {
        [Inject]
        private GameDefinitionModel gameDefinitionModel;
        
        [Inject] 
        private IResourceService resourceService;
        
        public IEnumerator Initialize()
        {
            List<ResourceVO> resources = new List<ResourceVO>
            {
                new ResourceVO
                {
                    Amount = 18,
                    ResourceDefinition = gameDefinitionModel.GetOrCreate<ResourceDefinition>(ResourceConstants.SoftCurrency)
                },
                new ResourceVO
                {
                    Amount = 222,
                    ResourceDefinition = gameDefinitionModel.GetOrCreate<ResourceDefinition>(ResourceConstants.HardCurrency)
                }
            };
            
            resourceService.InitializeResources(resources);
            
            yield return null;
        }
    }
}