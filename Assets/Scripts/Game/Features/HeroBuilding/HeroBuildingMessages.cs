using Core;
using Game.Features.Heroes;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Features.HeroBuilding
{
	public static class HeroBuildingMessages
	{
		public class HeroBuildingMessage : IMessage
		{
			public HeroVO Hero { get; }
			public float3 Position { get; }

			public HeroBuildingMessage(HeroVO hero, float3 position)
			{
				Hero = hero;
				Position = position;
			}
		}

		public class HeroTryBuildingMessage : IMessage
		{
			public HeroClassVO HeroClassVO { get; }

			public HeroTryBuildingMessage(HeroClassVO heroClassVO)
			{
				HeroClassVO = heroClassVO;
			}
		}

		public class ClickOnBuldingPlaceMessage : IMessage
		{
			public Transform Transform { get; }

			public ClickOnBuldingPlaceMessage(Transform transform)
			{
				Transform = transform;
			}
		}

		public class OnCompleteHeroBuildingMessage : IMessage
		{
			public bool IsBuilding { get; }
			public GameObject HeroGO { get; }

			public OnCompleteHeroBuildingMessage(bool isBuilding, GameObject heroGO)
			{
				IsBuilding = isBuilding;
				HeroGO = heroGO;
			}
		}
	}
}