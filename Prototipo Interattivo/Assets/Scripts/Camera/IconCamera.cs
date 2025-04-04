using UnityEngine;

public class IconCamera : MonoBehaviour
{
	private Transform objectToRender;
	[SerializeField] private float offset;

	private void Start()
	{
		objectToRender = GameManager.Instance.playerController.CameraPivot;
	}

	private void Update()
	{
		// TODO: find a way for distance to be constant no matter if you are looking up or down
		transform.position = objectToRender.position + objectToRender.forward * offset;
		transform.position = new Vector3(transform.position.x, objectToRender.position.y, transform.position.z);
		transform.LookAt(objectToRender);
	}
}
