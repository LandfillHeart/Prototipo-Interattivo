using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private float cameraSensitivity;
	[SerializeField] private float verticalOffsetTPS;
	[SerializeField] private float distanceFromPlayerTPS;
	[SerializeField] private float horizontalOffsetTPS;
	private PlayerController player;

	public float DistanceFromPlayer => distanceFromPlayerTPS;

	private Vector3 cameraVerticalOffset = Vector3.zero;

	private enum CameraMode
	{
		ThirdPersonStatic,
		FirstPerson,
		Dynamic
	}

	private void Start()
	{
		player = GameManager.Instance.playerController;
		cameraVerticalOffset.y = verticalOffsetTPS;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		cam.transform.position = player.transform.position + cameraVerticalOffset + player.transform.forward * -1 * distanceFromPlayerTPS + player.transform.right * horizontalOffsetTPS;
		
	}

	private void Update()
	{
		ThirdPerson();
	}

	private void ThirdPerson()
	{
		// camera needs to always be above the player to look "over the shoulder" (player.position + cameraVerticalOffset)
		// camera needs to be slightly offset to make it easier to see around/aim without player model blocking center of screen (cameraPivot.right * horizontalOffsetTPS)
		// camera needs to be placed behind the player and relative to where they are looking (cameraPivot.forward * -1 * distanceFromPlayerTPS)
		cam.transform.position = player.transform.position + cameraVerticalOffset + player.CameraPivot.transform.forward * -1 * distanceFromPlayerTPS + player.CameraPivot.transform.right * horizontalOffsetTPS;
		cam.transform.LookAt(player.CameraPivot.transform.forward * 100f);
	}

	public void RotateCamera(Vector2 mouseDelta)
	{
		// scale by mouseSentivity - let player customize experience. Scale by deltaTime - mouse sensitivity doesn't change with fluctating FPS
		mouseDelta *= cameraSensitivity * Time.deltaTime;
		player.transform.Rotate(new Vector3(0, mouseDelta.x, 0));

		float newRot = player.CameraPivot.transform.rotation.eulerAngles.x - mouseDelta.y;
		if (newRot > 0 && newRot < 65)
		{
			player.CameraPivot.Rotate(new Vector3(-mouseDelta.y, 0, 0));
		}

		if(newRot < 0 || newRot > 270)
		{
			player.CameraPivot.Rotate(new Vector3(-mouseDelta.y, 0, 0));
		}
	}
}