using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;
	public static UIManager Instance => instance;

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


}
