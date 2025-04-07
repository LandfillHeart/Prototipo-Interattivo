using UnityEngine;

public class SetRespawnTrigger : MonoBehaviour
{
	[SerializeField] private Vector3 newRespawnPoint;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GameManager.Instance.RespawnPoint = newRespawnPoint;
		}
	}
}
