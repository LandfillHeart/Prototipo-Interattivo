using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	// TO-DO add an inventory item type and set it as an optional for interactions
	[SerializeField] private bool interactionItemRequired;

	public Action interactionSuccess;
	public Action interactionFail;

	public bool ConditionsMet => InteractionsEnabled;
	private bool interactionsEnabled = true;
	public bool InteractionsEnabled
	{
		get => interactionsEnabled;
		set => interactionsEnabled = value;
	}

	public void AttemptInteraction()
	{
		if(ConditionsMet)
		{
			interactionSuccess?.Invoke();
			return;
		}

		interactionFail?.Invoke();

	}

	

}
