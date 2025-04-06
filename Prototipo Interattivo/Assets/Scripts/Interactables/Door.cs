using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Interactable))]
public class Door : MonoBehaviour
{
	[Serializable]
	public class DoorDetails
	{
		public Transform doorTransform;
		public Vector3 startRotation;
		public Vector3 endRotation;
	}
	[SerializeField] private DoorDetails[] doorDetails;
	[SerializeField] private float rotationTime;
	
	private Interactable interactable;

	private bool coroutineRunning;

	private void Start()
	{
		interactable = GetComponent<Interactable>();
		interactable.interactionSuccess += TryOpen;
	}

	private void TryOpen()
	{
		if (!coroutineRunning)
		{ 
			StartCoroutine(ToggleOpen());
		}
	}

	private IEnumerator ToggleOpen()
	{
		coroutineRunning = true;
		float elapsedTime = 0f;
		int doorsToRotate = doorDetails.Length;
		while(elapsedTime < rotationTime)
		{
			elapsedTime += Time.deltaTime;
			for(int i = 0; i < doorDetails.Length; i++)
			{
				doorDetails[i].doorTransform.localRotation = Quaternion.Euler(Vector3.Lerp(doorDetails[i].startRotation, doorDetails[i].endRotation, elapsedTime / rotationTime));
			}
			yield return null;
		}
		coroutineRunning = false;
	}
}
