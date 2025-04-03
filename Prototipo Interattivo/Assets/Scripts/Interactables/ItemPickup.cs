using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	private Interactable interactable;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		interactable = GetComponent<Interactable>();
		interactable.interactionSuccess += PickupItem;
	}

	private void PickupItem()
	{

	}

}
