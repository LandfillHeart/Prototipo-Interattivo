using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	[SerializeField] private ItemData itemData;
	private Interactable interactable;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		interactable = GetComponent<Interactable>();
		interactable.interactionSuccess += PickupItem;
	}

	private void PickupItem()
	{
		GameManager.Instance.playerEntity.inventory.AddItem(itemData);
	}

}
