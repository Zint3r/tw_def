using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Features.PlayerControl
{
	public interface IPlayerInputService
	{
		//event Action<Vector2> OnMousePositionContext;
		event Action OnRightMouseButtonContext;
		event Action OnLeftMouseButtonContext;
	}
	public class PlayerControlInput : MonoBehaviour, IPlayerInputService
	{
		private MyInputs _conntrol;
		private Vector2 mousePosition = new Vector2();
		public Vector2 MousePosition { get => mousePosition; }

		//public event Action<Vector2> OnMousePositionContext;
		public event Action OnRightMouseButtonContext;
		public event Action OnLeftMouseButtonContext;

		private void OnEnable()
		{
			_conntrol.Enable();
		}
		private void OnDisable()
		{
			_conntrol.PC.Move.performed -= MousePositionContext;
			_conntrol.PC.Move.canceled -= MousePositionContext;
			_conntrol.PC.MoveButton.performed -= RightMouseButtonContext;
			_conntrol.PC.TargetButton.performed -= LeftMouseButtonContext;
			_conntrol.Disable();
		}
		private void Awake()
		{
			_conntrol = new MyInputs();
			_conntrol.PC.Move.performed += MousePositionContext;
			_conntrol.PC.Move.canceled += MousePositionContext;
			_conntrol.PC.MoveButton.performed += RightMouseButtonContext;
			_conntrol.PC.TargetButton.performed += LeftMouseButtonContext;
		}
		private void MousePositionContext(InputAction.CallbackContext context)
		{
			mousePosition = context.ReadValue<Vector2>();
			//OnMousePositionContext?.Invoke(context.ReadValue<Vector2>());
		}
		private void RightMouseButtonContext(InputAction.CallbackContext context)
		{
			OnRightMouseButtonContext?.Invoke();
		}
		private void LeftMouseButtonContext(InputAction.CallbackContext context)
		{
			OnLeftMouseButtonContext?.Invoke();
		}
	}
}