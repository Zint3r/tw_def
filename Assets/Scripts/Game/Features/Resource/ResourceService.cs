using Core.MessageHub;
using System.Collections.Generic;
using System.Diagnostics;
using Zenject;

namespace Game.Features.Resource
{
	public interface IResourceService
    {
        void InitializeResources(List<ResourceVO> resources);
        void UpdateResources(List<ResourceVO> resources);
        void PredictResourceChange(List<ResourceVO> changedResources, bool isSendingMessage = true, bool showFeedbackImmediately = false);
        void PredictResourceChange(ResourceVO resource, bool isSendingMessage = true);
		void UpdateResource(ResourceVO resource);
	}
    
    public class ResourceService : IResourceService
    {
        [Inject]
        private IMessageHubService messageHubService;
        
        [Inject]
        private ResourceModel resourceModel;
        
        public void InitializeResources(List<ResourceVO> resources)
        {
            foreach (ResourceVO resourceVO in resources)
            {
                resourceModel.Resources[resourceVO.ResourceDefinition.DefinitionId] = resourceVO;
            }
        }        
        public void UpdateResources(List<ResourceVO> resources)
        {
            List<ResourceVO> changedResources = new List<ResourceVO>();
            foreach (ResourceVO resourceVO in resources)
            {
				ResourceVO changedResource;
                if (resourceModel.Resources.TryGetValue(resourceVO.ResourceDefinition.DefinitionId, out ResourceVO existingResource))
                {
                    changedResource = new ResourceVO
					{
                        Amount = existingResource.Amount - resourceVO.Amount,
                        ResourceDefinition = resourceVO.ResourceDefinition
                    };
                }
                else
                {
                    changedResource = resourceVO;
                }

                resourceModel.Resources[resourceVO.ResourceDefinition.DefinitionId] = changedResource;
				
                changedResources.Add(changedResource);
            }

            messageHubService.Publish(new ResourceMessages.ResourceUpdatedMessage(changedResources));
        }
		public void UpdateResource(ResourceVO resource)
		{
			if (resourceModel.Resources.TryGetValue(resource.ResourceDefinition.DefinitionId, out ResourceVO existingResource) == true)
            {
				resourceModel.Resources[resource.ResourceDefinition.DefinitionId] = resource;
				messageHubService.Publish(new ResourceMessages.ResourceUpdatedMessage(resource));
			}
		}
		public void PredictResourceChange(List<ResourceVO> changedResources, bool isSendingMessage = true, bool showFeedbackImmediately = false)
        {
            foreach (ResourceVO resource in changedResources)
            {
                PredictResourceChange(resource, false);
            }

            if (isSendingMessage)
            {
                messageHubService.Publish(new ResourceMessages.ResourceUpdatedMessage(changedResources, showFeedbackImmediately));
            }
        }        
        public void PredictResourceChange(ResourceVO resource, bool isSendingMessage = true)
        {
            if (resourceModel.Resources.ContainsKey(resource.ResourceDefinition.DefinitionId))
            {
				ResourceVO tempResource = resourceModel.Resources[resource.ResourceDefinition.DefinitionId];
                tempResource.Amount += resource.Amount;
                resourceModel.Resources[resource.ResourceDefinition.DefinitionId] = tempResource;
				if (isSendingMessage)
				{
					messageHubService.Publish(new ResourceMessages.ResourceUpdatedMessage(tempResource));
				}
			}
            else
            {
                resourceModel.Resources[resource.ResourceDefinition.DefinitionId] = resource;
				if (isSendingMessage)
				{
					messageHubService.Publish(new ResourceMessages.ResourceUpdatedMessage(resource));
				}
			}
        }
    }
}