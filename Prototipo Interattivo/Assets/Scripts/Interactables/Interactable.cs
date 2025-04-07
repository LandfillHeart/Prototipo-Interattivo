using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	// TO-DO add an inventory item type and set it as an optional for interactions
	[Header("Animations")]
	[SerializeField] private bool playInteractionAnimationOnSuccess;
	[SerializeField] private bool playInteractionAnimationOnFailure;
	[Header("Interaction Settings")]
	[SerializeField] private bool interactionItemRequired;
	[Tooltip("This is only used when interacionItemRequired is set to true")] [SerializeField] private ItemData requiredItem;
	[SerializeField] private bool limitedInteractions;
	[Tooltip("This is only used when limitedInteractions is true")]
	[SerializeField] private int maxSuccessfulInteractions;

	[Header("UI Prompts")]
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
		if(ConditionsMet && ItemRequirementMet(entity))
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

	private bool ItemRequirementMet(LandfillEntity entity)
	{
		if (!interactionItemRequired) return true;
		if (entity.Inventory.items.Count == 0) return false;
		return (entity.Inventory.CurrentItem == requiredItem);

	}

}
