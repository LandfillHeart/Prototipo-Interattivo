using UnityEngine;

[RequireComponent (typeof(Movement))]
public class LandfillEntity : MonoBehaviour
{
	public Movement movement;
	private void Awake()
	{
		movement = GetComponent<Movement>();
	}
}
