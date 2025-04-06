using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;
	public static UIManager Instance => instance;

	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private TalkingHeadUI talkingHeadUI; 
	[SerializeField] private TextMeshProUGUI interactionPrompt;
	 
	private void Awake()
	{
		instance = this;
	}

	public void SetInteractionPrompt(Interactable interactable)
	{
		ToggleInteractionPrompt(true);
		if (interactable.MaxInteractionsReached)
		{
			interactionPrompt.text = interactable.maxInteractionsReachedPrompt;
			return;
		}

		interactionPrompt.text = interactable.interactionPromptPrefix + " " + interactable.interactionPrompt;
		
	}

	public void ToggleInteractionPrompt(bool state) { 
		interactionPrompt.enabled = state;
	}

	public void ToggleTalkingHeadUI(bool state)
	{
		talkingHeadUI.Toggle(state);
	}

	public void SetSpeechBubbleContent(string newContent)
	{
		if(newContent == null || newContent.Length == 0)
		{
			//ToggleTalkingHeadUI(false);
			return;
		}
 		talkingHeadUI.SetSpeechBubbleContent(newContent);
	}

}
