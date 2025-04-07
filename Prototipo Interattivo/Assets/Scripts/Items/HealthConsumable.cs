using UnityEngine;

public class HealthConsumable : MonoBehaviour
{
	[SerializeField] private float healthRestored;
	private Item item;

	private void Start()
	{
		item = GetComponent<Item>();
		item.itemUsed += OnUse;
	}

	private void OnUse()
	{
		item.ItemUser.Health.Heal(healthRestored);
		item.ItemUser.Interact(true);
	}

}
