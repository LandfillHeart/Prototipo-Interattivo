using UnityEngine;

public class LandfillAnimator : MonoBehaviour, IEntityComponent
{
	[SerializeField] private Animator animator;

	private LandfillEntity parentEntity;
	public LandfillEntity ParentEntity => parentEntity;

	[HideInInspector] public bool animationLocked = false;

	private string lockingAnimation;

	private void Awake()
	{
		parentEntity = GetComponent<LandfillEntity>();
	}

	private void Update()
	{
		if (animationLocked && animator.GetCurrentAnimatorStateInfo(0).IsName(lockingAnimation) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			animationLocked = false;
			// animator.CrossFade("Idle", 0f);
			parentEntity.MovementLocked = false;
		}
	}

	// Other components should only send requests to LandfillAnimator to play an animation
	// Whether the animation is played or not cannot be directly controlled by the other components, but instead by the animator and entity's state machine
	public void PlayIdleAnimation()
	{
		if (animationLocked || parentEntity.MovementLocked || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Interact") || animator.IsInTransition(0)) return;
		animator.CrossFade("Idle", 0.1f);
	}

	public void PlayWalkAnimation()
	{
		if (animationLocked || parentEntity.MovementLocked || animator.GetCurrentAnimatorStateInfo(0).IsName("Walking_A") || animator.IsInTransition(0)) return;
		animator.CrossFade("Walking_A", 0.1f);
	}

	public void PlayRunAnimation()
	{
		if (animationLocked || parentEntity.MovementLocked || animator.GetCurrentAnimatorStateInfo(0).IsName("Running_A") || animator.IsInTransition(0)) return;
		animator.CrossFade("Running_A", 0.1f);
	}

	public void PlayInteractAnimation()
	{
		if (animationLocked || animator.GetCurrentAnimatorStateInfo(0).IsName("Interact")) return;
		animator.CrossFade("Interact", 0.1f);
		animationLocked = true;
		lockingAnimation = "Interact";
	}

	public void PlayDeathAnimation()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lie_Down")) return;
		animator.CrossFade("Lie_Down", 0.1f);
		animationLocked = true;
		lockingAnimation = "Lie_Down";
	}

}