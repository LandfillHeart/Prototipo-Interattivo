using UnityEngine;

[RequireComponent (typeof(CameraController))]
public class InteractionManager : MonoBehaviour
{
	[SerializeField] private LandfillEntity playerEntity;
	[SerializeField] private float maxInteractionDistance;

	private CameraController cameraController;

	private LayerMask interactableLayer;
	private Interactable lastHitInteractable;

	private bool inInteractionRange;

	private void Start()
	{
		cameraController = GetComponent<CameraController>();
		interactableLayer = LayerMask.GetMask("Interactable");
	}

	// This function allows us to check if we are within range to interact with an item in the scene and caches the result to prevent constant GetComponent calls
	private void FixedUpdate()
	{
		RaycastHit rayHit;
		if (!Physics.Raycast(transform.position, transform.forward, out rayHit, maxInteractionDistance + cameraController.DistanceFromPlayer, interactableLayer))
		{
			inInteractionRange = false;
			// TOGGLE OFF INTERACTABLE HUD
			UIManager.Instance.ToggleInteractionPrompt(false);
			return;
		}

		inInteractionRange = true;

		if(lastHitInteractable != null && rayHit.collider.gameObject == lastHitInteractable.gameObject)
		{
			// TOGGLE ON INTERACTABLE HUD - CONTENT IS CACHED IN UIMANAGER
			UIManager.Instance.ToggleInteractionPrompt(true);
			return;
		}

		rayHit.collider.TryGetComponent<Interactable>(out lastHitInteractable);
		// TOGGLE ON INTERACTABLE HUD AND SET CONTENT
		UIManager.Instance.SetInteractionPrompt(lastHitInteractable);
	}

	public void AttemptInteract()
	{
		if (!inInteractionRange)
		{
			return;
		}

		if(!lastHitInteractable)
		{
			return;
		}

		lastHitInteractable.AttemptInteraction(playerEntity);

	}

}
