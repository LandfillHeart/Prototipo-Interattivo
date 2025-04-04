using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
	[SerializeField] private Image leftItem;
	[SerializeField] private Image equippedItem;
	[SerializeField] private Image rightItem;

	private Inventory playerInventory;

	private void Start()
	{
		playerInventory = GameManager.Instance.playerEntity.inventory;
		playerInventory.itemSwitched += UpdateInventoryUI;
	}

	private void UpdateInventoryUI()
	{
		leftItem.sprite = playerInventory.PreviousItem.itemIcon;
		equippedItem.sprite = playerInventory.CurrentItem.itemIcon;
		rightItem.sprite = playerInventory.NextItem.itemIcon;
	}

}