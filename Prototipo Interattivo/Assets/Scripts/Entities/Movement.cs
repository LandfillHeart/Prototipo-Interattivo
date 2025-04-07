using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, IEntityComponent
{
	[SerializeField] private float walkingSpeed = 1f;
	[SerializeField] private float sprintSpeed = 3f;

	[SerializeField] private float jumpHeight = 10f;

	private LandfillEntity parentEntity;
	public LandfillEntity ParentEntity => parentEntity;

	public float WalkingSpeed
	{
		get => walkingSpeed;
		set => walkingSpeed = value;
	}

	public float SprintSpeed
	{
		get => sprintSpeed;
		set => sprintSpeed = value;
	}

	public Vector3 MovementDirection
	{
		get => movementDirection;
		set => movementDirection = value;
	}

	public bool ShouldJump
	{
		set => shouldJump = value;
	}

	private Rigidbody rb;

	private Vector3 movementDirection = Vector3.zero;

	public Vector3 horizontalVelocity = Vector3.zero;

	private bool shouldJump = false;

	private Vector3 relativeVelocity = Vector3.zero;

	public enum SpeedState
	{
		Walk,
		Sprint
	}

	private SpeedState currentSpeedState;
	public SpeedState CurrentSpeedState
	{
		get => currentSpeedState;
		set => currentSpeedState = value;
	}
	private float currentMaxSpeed => currentSpeedState switch
	{
		SpeedState.Walk => walkingSpeed,
		SpeedState.Sprint => sprintSpeed,
		_ => walkingSpeed,
	};

	private void Awake()
	{
		parentEntity = GetComponent<LandfillEntity>();
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		currentSpeedState = SpeedState.Walk;
		parentEntity.LandfillAnimator.PlayIdleAnimation();
	}

	private void FixedUpdate()
	{
		// velocity relative to the direction we are facing - rb.linearVelocity is relative to world coordinates (e.g. we could be walking forward, but in world coords we are moving backwards x or z < 0f)
		relativeVelocity = Quaternion.Inverse(transform.rotation) * rb.linearVelocity;
		horizontalVelocity.x = relativeVelocity.x;
		horizontalVelocity.z = relativeVelocity.z;
		// trying to prevent double jump inputs when spamming caused by frame vs fixed time
		if (shouldJump)
		{
			shouldJump = false;
			Jump();
		}

		if (!parentEntity.MovementLocked) Move(movementDirection); else Stop();

		// Unity gravity will always feel "floaty" because of exagerated jumps. Applies double gravity when falling
		// NOTE: currently removed this because it works best for FPS games, this project is TPS and the jump height is very short it's better to use unity's animation
		/*
		if (rb.linearVelocity.y < 0f)
		{
			rb.AddForce(Vector3.down * -Physics.gravity.y * 2, ForceMode.Acceleration);
		}*/

	}

	private void Move(Vector2 inputDirection)
	{
		// translate Unity Input System value Vector2 into a Vector3 for horizontal movement
		Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
		// do NOT use rb.velocity.magnitude, it takes into account vertical speed
		// we don't want our calculations messed with by jumping
		if (direction == Vector3.zero)
		{
			// try to force our stop when player stops pressing inputs - remove slide
			Stop();
			return;
		}

		switch(CurrentSpeedState)
		{
			case SpeedState.Walk:
				parentEntity.LandfillAnimator.PlayWalkAnimation();
				break;
			case SpeedState.Sprint:
				parentEntity.LandfillAnimator.PlayRunAnimation();
				break;
		}

		if (horizontalVelocity.magnitude > currentMaxSpeed)
		{
			// opposing force to prevent you from moving faster than a desired speed (and without altering rb.velocity directly!)
			rb.AddRelativeForce(-horizontalVelocity.normalized * (horizontalVelocity.magnitude - currentMaxSpeed), ForceMode.VelocityChange);
			return;
		}
		rb.AddRelativeForce(direction.normalized * currentMaxSpeed, ForceMode.VelocityChange);
	}

	// This allows LandfillEntity to stop itself without relying on fixed time logic
	public void Stop()
	{
		rb.AddRelativeForce(-horizontalVelocity.normalized * (horizontalVelocity.magnitude), ForceMode.VelocityChange);
		parentEntity.LandfillAnimator.PlayIdleAnimation();
	}

	private void Jump()
	{
		if (!IsGrounded() || parentEntity.MovementLocked)
		{
			return;
		}
		Vector3 jumpDirection = new Vector3(0, 1, 0);
		rb.AddForce(jumpDirection * jumpHeight, ForceMode.VelocityChange);
	}

	private bool IsGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, 1.1f);
	}

	public void ToggleSprint()
	{
		if(CurrentSpeedState == SpeedState.Walk)
		{
			CurrentSpeedState = SpeedState.Sprint;
			return;
		}

		CurrentSpeedState = SpeedState.Walk;

	}

}
