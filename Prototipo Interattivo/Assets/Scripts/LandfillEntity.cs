using UnityEngine;

[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(LandfillAnimator))]
public class LandfillEntity : MonoBehaviour
{
	public Movement movement;
	public LandfillAnimator landfillAnimator;
	public Inventory inventory;
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
				movement.Stop();
			}
		}
	}
	private void Awake()
	{
		movement = GetComponent<Movement>();
		landfillAnimator = GetComponent<LandfillAnimator>();
		inventory = GetComponent<Inventory>();
	}

	public void Interact(bool playAnimation)
	{
		if(!playAnimation)
		{
			return;
		}
		MovementLocked = true;
		landfillAnimator.PlayInteractAnimation();
		
	}

}