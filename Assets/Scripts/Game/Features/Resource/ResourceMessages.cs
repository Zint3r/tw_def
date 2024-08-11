using Core;
using System.Collections.Generic;

namespace Game.Features.Resource
{
	public static class ResourceMessages
    {
        public struct ResourceUpdatedMessage : IMessage
        {
            public readonly List<ResourceVO> ChangedResources;

            public readonly bool ShowFeedbackImmediately;
			
            public ResourceUpdatedMessage(List<ResourceVO> changedResources, bool showFeedbackImmediately = false)
            {
                ChangedResources = changedResources;
                ShowFeedbackImmediately = showFeedbackImmediately;
            }
            
            public ResourceUpdatedMessage(ResourceVO changedResource, bool showFeedbackImmediately = false) :
                this(new List<ResourceVO>(1){ changedResource }, showFeedbackImmediately)
            {

            }
        }
    }
}