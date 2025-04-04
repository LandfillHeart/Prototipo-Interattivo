using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance => instance;

	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform playerSpawnPoint;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private InteractionManager interactionManager;
	
	[NonSerialized] public LandfillEntity playerEntity;
	[NonSerialized] public PlayerController playerController;

	
	private void Awake()
	{
		instance = this;
		SetupPlayer();
	}

	private void SetupPlayer()
	{
		playerEntity = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<LandfillEntity>();
		playerController = playerEntity.GetComponent<PlayerController>();
		playerController.cameraController = cameraController;
		playerController.interactionManager = interactionManager;
	}

}