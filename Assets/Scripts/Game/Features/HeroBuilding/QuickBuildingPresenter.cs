using Core.MessageHub;
using Game.Features.PlayerControl;
using UnityEngine;
using Zenject;

namespace Game.Features.HeroBuilding
{
	public class QuickBuildingPresenter : MonoBehaviour
	{
		[Inject]
		private IMessageHubService messageHubService;

		[Inject]
		private PlayerControlInput playerControlInput;

		private new Camera camera;
		private int layerMask = 1 << 7;

		public void OnStart()
		{			
			camera = Camera.main;
			playerControlInput.OnLeftMouseButtonContext += CheckClickOnBuldingPlace;
		}
		private void OnDestroy()
		{
			playerControlInput.OnLeftMouseButtonContext -= CheckClickOnBuldingPlace;
		}
		private void CheckClickOnBuldingPlace()
		{
			Ray ray = camera.ScreenPointToRay(playerControlInput.MousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
			{
				messageHubService.Publish(new HeroBuildingMessages.ClickOnBuldingPlaceMessage(hit.collider.gameObject.transform));
			}
		}
	}
}