using UnityEngine;

[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(LandfillAnimator))]
public class LandfillEntity : MonoBehaviour
{
	public Movement movement;
	public LandfillAnimator landfillAnimator;
	private void Awake()
	{
		movement = GetComponent<Movement>();
		landfillAnimator = GetComponent<LandfillAnimator>();
	}
}