using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item Data")]
public class ItemData : ScriptableObject
{
	public Sprite itemIcon;
	public string itemName;

	public GameObject itemPrefab;

}