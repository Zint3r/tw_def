using Core;
using System.Collections.Generic;
using UnityEngine;

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

		public class HeroAttackMessage : IMessage
		{
			public Transform TargetTransform { get; }
			public Vector3 HeroPosition { get; }
			public HeroDamageInfo HeroDamageInfo { get; }
			public bool IsCrit { get; }

			public HeroAttackMessage(Transform target, Vector3 heroPosition, HeroDamageInfo heroDamageInfo)
			{
				TargetTransform = target;
				HeroPosition = heroPosition;
				HeroDamageInfo = heroDamageInfo;
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