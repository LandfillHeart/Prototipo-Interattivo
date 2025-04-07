using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
	[TextArea(1, 20)]
	[SerializeField] private string dialogue;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag != "Player") 
		{ 
			return; 
		}
		
		UIManager.Instance.SetSpeechBubbleContent(dialogue);
	
	}
}
