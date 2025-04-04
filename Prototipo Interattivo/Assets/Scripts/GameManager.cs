using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance => instance;

	[Header("Player")]
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform playerSpawnPoint;

	[Header("Camera")]
	[SerializeField] private CameraController cameraController;
	[SerializeField] private InteractionManager interactionManager;

	[Header("Dialogues")]
	[SerializeField] private DialogueData tutorialDialogue;

	[NonSerialized] public LandfillEntity playerEntity;
	[NonSerialized] public PlayerController playerController;

	private void Awake()
	{
		instance = this;
		SetupPlayer();
	}

	private void Start()
	{
		UIManager.Instance.SetSpeechBubbleContent(tutorialDialogue.dialogues[0]);
	}

	private void SetupPlayer()
	{
		playerEntity = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<LandfillEntity>();
		playerController = playerEntity.GetComponent<PlayerController>();
		playerController.cameraController = cameraController;
		playerController.interactionManager = interactionManager;
	}

}