using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	// TO-DO add an inventory item type and set it as an optional for interactions\
	[SerializeField] private bool playInteractionAnimationOnSuccess;
	[SerializeField] private bool playInteractionAnimationOnFailure;
	[SerializeField] private bool interactionItemRequired;
	[SerializeField] private bool limitedInteractions;
	[Tooltip("This is only used when limitedInteractions is true")]
	[SerializeField] private int maxSuccessfulInteractions;


	[Tooltip("Replaces the Press F to text in HUD")] [SerializeField] public string interactionPromptPrefix = "Press F to";
	[SerializeField] public string interactionPrompt = "Interact";
	[SerializeField] public string maxInteractionsReachedPrompt;

	public Action interactionSuccess;
	public Action interactionFail;

	private int successfulInteractions;
	private bool interactionsEnabled = true;

	public bool ConditionsMet {
		get {
			if (!InteractionsEnabled) return false;
			if (MaxInteractionsReached) return false;
			return true;
		}
	}

	public bool MaxInteractionsReached => limitedInteractions && successfulInteractions >= maxSuccessfulInteractions;


	public bool InteractionsEnabled
	{
		get => interactionsEnabled;
		set => interactionsEnabled = value;
	}

	public void AttemptInteraction(LandfillEntity entity)
	{
		if(ConditionsMet)
		{
			interactionSuccess?.Invoke();
			successfulInteractions++;
			UIManager.Instance.SetInteractionPrompt(this);
			entity.Interact(playInteractionAnimationOnSuccess);
			return;
		}

		interactionFail?.Invoke();
		UIManager.Instance.SetInteractionPrompt(this);
		entity.Interact(playInteractionAnimationOnFailure);
	}

}
