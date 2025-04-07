using UnityEngine;

public class SimpleFloatAnimation : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float hoverHeight;

	private Vector3 posCache;
	private float startingY;

	private void Start()
	{
		posCache = transform.localPosition;
		startingY = transform.localPosition.y;
	}

	private void FixedUpdate()
	{
		posCache.y = startingY + hoverHeight * Mathf.Sin(Time.fixedTime * speed);
		transform.localPosition = posCache;
	}
}
