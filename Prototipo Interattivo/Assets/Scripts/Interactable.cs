using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	// TO-DO add an inventory item type and set it as an optional for interactions
	[SerializeField] private bool interactionItemRequired;
	[SerializeField] private bool limitedInteractions;
	[Tooltip("This is only used when limitedInteractions is true")]
	[SerializeField] private int maxSuccessfulInteractions;

	public Action interactionSuccess;
	public Action interactionFail;

	private int successfulInteractions;

	public bool ConditionsMet {
		get {
			if (!InteractionsEnabled) return false;
			if (limitedInteractions && successfulInteractions >= maxSuccessfulInteractions) return false;
			return true;
		}
	}

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
			successfulInteractions++;
			return;
		}

		interactionFail?.Invoke();

	}

	

}
