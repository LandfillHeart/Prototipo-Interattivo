using System;
using UnityEngine;

public class Health : MonoBehaviour, IEntityComponent
{
	[SerializeField] private float maxHealth;
	[SerializeField] private float startingHealth;
	private float currentHealth;

	public Action die;
	public Action healthChanged;

	private LandfillEntity parentEntity;
	public LandfillEntity ParentEntity => parentEntity;

	private void Awake()
	{
		parentEntity = GetComponent<LandfillEntity>();
		CurrentHealth = startingHealth;
	}

	public float MaxHealth
	{
		get { return maxHealth; }
		set
		{
			maxHealth = Mathf.Max(0f, value);
			CurrentHealth = Mathf.Min(CurrentHealth, value);
		}
	}

	public float CurrentHealth
	{
		get { return currentHealth; }
		set
		{
			currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
			healthChanged?.Invoke();
			if(currentHealth == 0f)
			{
				die?.Invoke();
			}
		}
	}

	public void Heal(float amount)
	{
		CurrentHealth += amount;
	}

	public void Damage(float amount)
	{
		CurrentHealth -= amount;
	}

}