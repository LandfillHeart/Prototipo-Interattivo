using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActionAsset;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private LandfillEntity playerEntity;

	[SerializeField] private Transform cameraPivot;

	public Transform CameraPivot => cameraPivot;

	private InputActionMap playerActionMap;
	private InputAction moveAction;
	private InputAction sprintToggleAction;
	private InputAction jumpAction;

	private InputAction lookAction;

	private void Start()
	{
		playerActionMap = inputActionAsset.FindActionMap("Player");
		
		lookAction = playerActionMap.FindAction("Look");
		moveAction = playerActionMap.FindAction("Move");
		sprintToggleAction = playerActionMap.FindAction("Sprint");
		jumpAction = playerActionMap.FindAction("Jump");

		lookAction.performed += ctx => cameraController.RotateCamera(ctx.ReadValue<Vector2>());
		moveAction.performed += ctx => playerEntity.movement.MovementDirection = ctx.ReadValue<Vector2>();
		moveAction.canceled += ctx => {
			playerEntity.movement.MovementDirection = Vector2.zero;
			playerEntity.movement.CurrentSpeedState = Movement.SpeedState.Walk;
		};

		sprintToggleAction.started += ctx => playerEntity.movement.ToggleSprint();

		jumpAction.started += ctx => playerEntity.movement.ShouldJump = true;

	}
}
