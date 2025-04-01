using UnityEngine;

[RequireComponent (typeof(Interactable))]
public class Door : MonoBehaviour
{
	private Interactable interactable;
	private void Start()
	{
		interactable = GetComponent<Interactable>();
		interactable.interactionSuccess += ToggleOpen;
	}

	private void ToggleOpen()
	{
		Debug.Log("you did it!! you opened or closed the door!! now actually code the door animation silly");
	}
}