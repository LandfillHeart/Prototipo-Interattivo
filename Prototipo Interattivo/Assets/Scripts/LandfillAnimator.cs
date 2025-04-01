using UnityEngine;

public class LandfillAnimator : MonoBehaviour, IEntityComponent
{
	[SerializeField] private Animator animator;

	private LandfillEntity parentEntity;
	public LandfillEntity ParentEntity => parentEntity;

	private bool animationLocked = false;

	private void Awake()
	{
		parentEntity = GetComponent<LandfillEntity>();
	}
	// Other components should only send requests to LandfillAnimator to play an animation
	// Whether the animation is played or not cannot be directly controlled by the other components, but instead by the animator and entity's state machine
	public void PlayIdleAnimation()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.IsInTransition(0) || animationLocked) return;
		animator.CrossFade("Idle", 0.1f);
	}

	public void PlayWalkAnimation()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking_A") || animator.IsInTransition(0) || animationLocked) return;
		animator.CrossFade("Walking_A", 0.1f);
	}

	public void PlayRunAnimation()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running_A") || animator.IsInTransition(0) || animationLocked) return;
		animator.CrossFade("Running_A", 0.1f);
	}


}