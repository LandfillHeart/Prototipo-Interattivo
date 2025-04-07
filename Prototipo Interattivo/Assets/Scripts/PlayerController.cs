using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActionAsset;

	[NonSerialized] public CameraController cameraController;
	[NonSerialized] public InteractionManager interactionManager;
	[SerializeField] private LandfillEntity playerEntity;

	[SerializeField] private Transform cameraPivot;

	public Transform CameraPivot => cameraPivot;

	private InputActionMap playerActionMap;
	private InputAction moveAction;
	private InputAction sprintToggleAction;
	private InputAction jumpAction;
	private InputAction lookAction;

	private InputAction interactAction;

	private InputAction switchItemNext;
	private InputAction switchItemPrevious;

	private void Start()
	{
		playerActionMap = inputActionAsset.FindActionMap("Player");
		
		lookAction = playerActionMap.FindAction("Look");
		moveAction = playerActionMap.FindAction("Move");
		sprintToggleAction = playerActionMap.FindAction("Sprint");
		jumpAction = playerActionMap.FindAction("Jump");
		interactAction = playerActionMap.FindAction("Interact");
		switchItemNext = playerActionMap.FindAction("SwitchItemNext");
		switchItemPrevious = playerActionMap.FindAction("SwitchItemPrevious");

		lookAction.performed += ctx => cameraController.RotateCamera(ctx.ReadValue<Vector2>());
		moveAction.performed += ctx => playerEntity.Movement.MovementDirection = ctx.ReadValue<Vector2>();
		moveAction.canceled += ctx => {
			playerEntity.Movement.MovementDirection = Vector2.zero;
			playerEntity.Movement.CurrentSpeedState = Movement.SpeedState.Walk;
		};

		sprintToggleAction.started += ctx => playerEntity.Movement.ToggleSprint();

		jumpAction.started += ctx => playerEntity.Movement.ShouldJump = true;

		interactAction.started += ctx => interactionManager.AttemptInteract();

		switchItemNext.started += ctx => playerEntity.Inventory.SwitchEquippedItem(true);
		switchItemPrevious.started += ctx => playerEntity.Inventory.SwitchEquippedItem(false);

	}

	public void ToggleMovementActions(bool state)
	{
		if (state)
		{
			moveAction.Enable();
			sprintToggleAction.Enable();
			jumpAction.Enable();
		}
		else
		{
			moveAction.Disable();
			sprintToggleAction.Disable();
			jumpAction.Disable();
		}
	}

	public void ToggleCameraActions(bool state)
	{
		if(state) lookAction.Enable(); else lookAction.Disable();
	}

	public void ToggleInteractionActions(bool state)
	{
		if (state) interactAction.Enable(); else interactAction.Disable();
	}

}
