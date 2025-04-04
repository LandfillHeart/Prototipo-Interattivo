using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Data/Dialogue Data")]
public class DialogueData : ScriptableObject
{
	[TextArea(1, 20)]
	public string[] dialogues;
}