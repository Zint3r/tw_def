using Unity.Burst;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class CameraMove : MonoBehaviour
{	
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform cameraTransform;

	private TransformAccessArray transformAccessArray;
	private JobHandle moveHandle;

	[BurstCompile]
	struct CameraMoveJob : IJobParallelForTransform
	{
		public float deltaTime;
		public float speed;
		public Vector2 cameraPos;
		public Vector2 playerPos;

		public void Execute(int index, TransformAccess transform)
		{
			Vector3 newplayerPos = (playerPos - cameraPos) * speed * deltaTime;
			transform.localPosition += newplayerPos;
		}
	}

	private void Start()
	{		
		Transform[] transforms = new Transform[2];
		transforms[0] = cameraTransform;
		transformAccessArray = new TransformAccessArray(transforms);
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		CameraMoveJob moveJob = new CameraMoveJob
		{
			deltaTime = Time.fixedDeltaTime,
			playerPos = playerTransform.position,
			cameraPos = cameraTransform.position,
			speed = 2f
		};

		moveHandle = moveJob.Schedule(transformAccessArray);
		moveHandle.Complete();
	}

	private void OnDestroy()
	{
		moveHandle.Complete();
		transformAccessArray.Dispose();
	}
}