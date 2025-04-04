using TMPro;
using UnityEngine;

public class TalkingHeadUI : MonoBehaviour
{
	[SerializeField] private GameObject graphicsParent;
	[SerializeField] private TextMeshProUGUI dialogueText;

	private void Awake()
	{
		Toggle(false);
	}

	public void Toggle(bool state)
	{
		graphicsParent.SetActive(state);
	}

	public void SetSpeechBubbleContent(string content)
	{
		Toggle(true);
		dialogueText.text = content;
	}

}