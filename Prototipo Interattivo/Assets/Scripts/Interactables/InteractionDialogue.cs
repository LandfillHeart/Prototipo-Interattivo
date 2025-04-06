using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDialogue : MonoBehaviour
{
	[SerializeField] private string interactionSuccessDialogue;
	[SerializeField] private string interactionFailureDialogue;
	private Interactable interactable;
	private void Start()
	{
		interactable = GetComponent<Interactable>();
		interactable.interactionSuccess += () => SetDialogue(interactionSuccessDialogue);
		interactable.interactionFail += () => SetDialogue(interactionFailureDialogue);
	}
	private void SetDialogue(string newDialogue) 
	{
		UIManager.Instance.SetSpeechBubbleContent(newDialogue);
	}
}
