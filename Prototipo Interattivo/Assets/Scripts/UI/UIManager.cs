using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;
	public static UIManager Instance => instance;

	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private TalkingHeadUI talkingHeadUI; 
	[SerializeField] private TextMeshProUGUI interactionPrompt;
	[SerializeField] private Image healthBar;

	private LandfillEntity player;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		player = GameManager.Instance.playerEntity;
		player.Health.healthChanged += UpdateHealthBar;
		UpdateHealthBar();
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

	public void UpdateHealthBar()
	{
		healthBar.fillAmount = player.Health.CurrentHealth / player.Health.MaxHealth;
	}

}
