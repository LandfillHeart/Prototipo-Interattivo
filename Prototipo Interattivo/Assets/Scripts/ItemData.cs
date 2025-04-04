using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class ItemData : ScriptableObject
{
	public Sprite itemIcon;
	public string itemName;

	public GameObject itemPrefab;

}