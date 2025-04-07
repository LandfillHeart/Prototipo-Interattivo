using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
	[SerializeField] private Vector3 startingPosition;
	[SerializeField] private Vector3 endPosition;
	[SerializeField] private float moveDuration;
	[SerializeField] private float pauseDuration;

	private Vector3 currentStartingPoint;
	private Vector3 currentEndPoint;

	private float elapsedMoveTime;
	private float elapsedPauseTime;

	private bool pause = true;
	private bool movingUp = true;

	private void Start()
	{
		currentStartingPoint = startingPosition;
		currentEndPoint = endPosition;
	}

	private void FixedUpdate()
	{
		if(pause)
		{
			PauseState();
		} else
		{
			MoveState();
		}
	}

	private void PauseState()
	{
		elapsedPauseTime += Time.fixedDeltaTime;
		if (elapsedPauseTime > pauseDuration) 
		{ 
			pause = false;
			elapsedMoveTime = 0f;
		}
	}

	private void MoveState()
	{
		elapsedMoveTime += Time.fixedDeltaTime;
		transform.position = Vector3.Lerp(currentStartingPoint, currentEndPoint, elapsedMoveTime / moveDuration);
		if (elapsedMoveTime > pauseDuration)
		{
			elapsedPauseTime = 0f;
			pause = true;
			if (movingUp)
			{
				movingUp = false;
				currentStartingPoint = endPosition;
				currentEndPoint = startingPosition;
			}
			else
			{
				movingUp = true;
				currentStartingPoint = startingPosition;
				currentEndPoint = endPosition;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		// kill entity when crushed by lift
		// in this prototype collider only includes entity layer, so getcomponent can't fail
		if(collision.transform.position.y < transform.position.y)
		{
			LandfillEntity entity = collision.gameObject.GetComponent<LandfillEntity>();
			entity.Health.Kill();
		}
	}

}
