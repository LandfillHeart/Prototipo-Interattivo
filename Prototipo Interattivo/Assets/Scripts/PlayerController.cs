using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActionAsset;

	[SerializeField] private CameraController cameraController;

	[SerializeField] private Transform cameraPivot;

	public Transform CameraPivot => cameraPivot;

	private InputActionMap playerActionMap;
	private InputAction moveAction;
	private InputAction jumpAction;

	private InputAction lookAction;

	private void Start()
	{
		playerActionMap = inputActionAsset.FindActionMap("Player");
		lookAction = playerActionMap.FindAction("Look");

		lookAction.performed += ctx => cameraController.RotateCamera(ctx.ReadValue<Vector2>());

	}
}
