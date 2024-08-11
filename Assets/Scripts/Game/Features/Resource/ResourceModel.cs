using Game.Features.GameDesign;
using Game.Utils;
using System.Collections.Generic;

namespace Game.Features.Resource
{
	public interface IResourceDataProvider
    {
		ResourceVO GetResource(string definitionId);
        List<ResourceVO> GetAllResources();
    }
    
    public class ResourceModel : IResourceDataProvider
    {
        public readonly Dictionary<string, ResourceVO> Resources = new Dictionary<string, ResourceVO>();
        
        public List<ResourceVO> GetAllResources()
        {
            return new List<ResourceVO>(Resources.Values);
        }
        
        public ResourceVO GetResource(string definitionId)
        {
            if (!Resources.TryGetValue(definitionId, out ResourceVO resourceVO))
            {
                resourceVO = new ResourceVO
				{
                    ResourceDefinition = DPL.GameDefinitionDataProvider.Get<ResourceDefinition>(definitionId),
                    Amount = 0
                };

                Resources[definitionId] = resourceVO;
            }

            return resourceVO;
        }
    }
}