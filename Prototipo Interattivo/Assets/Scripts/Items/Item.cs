using System;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private bool consumable;
	[SerializeField] private ItemData data;
	public ItemData Data => data;
	[HideInInspector] public LandfillEntity ItemUser;

	public Action itemUsed;

	public void Use()
	{
		itemUsed?.Invoke();
		if(consumable)
		{
			ItemUser.Inventory.RemoveItem(data);
		}
	}

}