using UnityEngine;

[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(LandfillAnimator))]
[RequireComponent (typeof(Inventory))]
[RequireComponent (typeof(Health))]
public class LandfillEntity : MonoBehaviour
{
	public Movement Movement;
	public LandfillAnimator LandfillAnimator;
	public Inventory Inventory;
	public Health Health;
	// I could probably toggle on/off movement actions, but for now I want to do it with just a bool to reduce risk of things breaking
	private bool movementLocked = false;
	public bool MovementLocked
	{
		get => movementLocked;
		set
		{
			movementLocked = value;
			if (value == true)
			{
				Movement.Stop();
			}
		}
	}
	private void Awake()
	{
		Movement = GetComponent<Movement>();
		LandfillAnimator = GetComponent<LandfillAnimator>();
		Inventory = GetComponent<Inventory>();
		Health = GetComponent<Health>();
	}

	public void Interact(bool playAnimation)
	{
		if(!playAnimation)
		{
			return;
		}
		MovementLocked = true;
		LandfillAnimator.PlayInteractAnimation();
		
	}

}