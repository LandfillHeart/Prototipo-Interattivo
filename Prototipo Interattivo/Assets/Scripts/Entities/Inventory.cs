using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IEntityComponent
{
	private LandfillEntity parentEntity;
	public LandfillEntity ParentEntity => parentEntity;

	[HideInInspector] [NonSerialized] public List<Item> items = new();
	[HideInInspector] [NonSerialized] public int equippedItemIndex = 0;

	[SerializeField] private Transform hand;

	public Action itemSwitched;
	public ItemData CurrentItem => items[equippedItemIndex].Data;

	public ItemData NextItem
	{
		get
		{
			if (equippedItemIndex + 1 == items.Count)
			{
				return items[0].Data;
			}
			return items[equippedItemIndex + 1].Data;
		}
	}

	public ItemData PreviousItem
	{
		get
		{
			if (equippedItemIndex - 1 < 0)
			{
				return items[items.Count - 1].Data;
			}

			return items[equippedItemIndex - 1].Data;
		}
	}

	private void Awake()
	{
		parentEntity = GetComponent<LandfillEntity>();
	}

	public void AddItem(ItemData data)
	{
		Item newItem = Instantiate(data.itemPrefab, hand).GetComponent<Item>();
		newItem.ItemUser = parentEntity;
		items.Add(newItem);
		newItem.gameObject.SetActive(false);
		if(items.Count == 1)
		{
			SwitchEquippedItem(true);
		}
		itemSwitched?.Invoke(); // this works well for updating the UI
	}

	public void RemoveItem()
	{
		Item itemCache = items[equippedItemIndex];
		items.RemoveAt(equippedItemIndex);
		Destroy(itemCache.gameObject);
		SwitchEquippedItem(shiftToRight: true, itemRemoved: true);
	}

	public void SwitchEquippedItem(bool shiftToRight, bool itemRemoved = false)
	{
		int indexShift = shiftToRight ? 1 : -1;
		if (itemRemoved) equippedItemIndex--;
		int newIndex = equippedItemIndex + indexShift;

		if (newIndex < 0)
		{
			newIndex = items.Count - 1;
		}

		if (newIndex >= items.Count) 
		{
			newIndex = 0;
		}

		if(!itemRemoved)
		{
			items[equippedItemIndex].gameObject.SetActive(false);
		}
		equippedItemIndex = newIndex;
		items[equippedItemIndex].gameObject.SetActive(true);
		itemSwitched?.Invoke();
	}

	public void UseEquippedItem()
	{
		if(items.Count == 0)
		{
			return;
		}

		items[equippedItemIndex].Use();

	}



}
