using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Game.Features.PlayerControl
{
	public class PlayerPresenter : MonoBehaviour
	{
		[Inject]
		private PlayerControlInput playerInput;

		private new Camera camera;
		//private GameObject targetObj;
		private NavMeshAgent agent;
		private int layerMask = 1 << 6;

		private void Awake()
		{
			var cinemashine = FindAnyObjectByType<CinemachineCamera>();
			cinemashine.Follow = gameObject.transform;
			cinemashine.LookAt = gameObject.transform;
			camera = Camera.main;
			agent = GetComponent<NavMeshAgent>();
		}
		private void Start()
		{
			playerInput.OnRightMouseButtonContext += RightMouseButtonContext;
		}
		private void OnDisable()
		{
			playerInput.OnRightMouseButtonContext -= RightMouseButtonContext;
		}
		private void RightMouseButtonContext()
		{
			//Debug.Log($"LeftMouseButtonClick on position: x - {_move.x} y - {_move.y}");
			Ray ray = camera.ScreenPointToRay(playerInput.MousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
			{
				if (agent.hasPath == true)
				{
					agent.isStopped = true;
					agent.ResetPath();
				}
		
				agent.destination = hit.point;
				agent.isStopped = false;
			}
		}
	}
}