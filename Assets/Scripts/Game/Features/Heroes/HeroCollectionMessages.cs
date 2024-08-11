using Core;
using System.Collections.Generic;

namespace Game.Features.Heroes
{
	public static class HeroCollectionMessages 
	{
		public class HeroActivateMessage : IMessage
		{
			public HeroVO Hero { get; }

			public HeroActivateMessage(HeroVO hero)
			{
				Hero = hero;
			}
		}

		public class HeroUpdatedMessage : IMessage
		{
			public HeroVO Hero { get; }

			public HeroUpdatedMessage(HeroVO hero)
			{
				Hero = hero;
			}
		}

		public struct HeroCollectionUpdatedMessage : IMessage
		{
			public readonly List<HeroVO> ChangedHeroes;

			public readonly bool ShowFeedbackImmediately;

			public HeroCollectionUpdatedMessage(List<HeroVO> changedResources, bool showFeedbackImmediately = false)
			{
				ChangedHeroes = changedResources;
				ShowFeedbackImmediately = showFeedbackImmediately;
			}
		}
	}
}