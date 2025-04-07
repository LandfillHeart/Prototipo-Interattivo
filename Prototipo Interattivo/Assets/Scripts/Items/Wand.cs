using UnityEngine;

public class Wand : MonoBehaviour
{
	[SerializeField] private float cooldown = 6f;
	[SerializeField] private float duration = 3f;
	[SerializeField] private float walkSpeedBuff = 1f;
	[SerializeField] private float sprintSpeedBuff = 3f;

	private float remainingCooldown = 0f;
	private float remainingDuration = 0f;

	private float defaultWalkSpeed;
	private float defaultSprintSpeed;

	private bool effectsApplied;

	private Item item;
	private void Start()
	{
		item = GetComponent<Item>();
		item.itemUsed += OnUse;
		defaultWalkSpeed = item.ItemUser.Movement.WalkingSpeed;
		defaultSprintSpeed = item.ItemUser.Movement.SprintSpeed;
	}

	private void Update()
	{
		remainingCooldown -= Time.deltaTime;

		if(effectsApplied)
		{
			remainingDuration -= Time.deltaTime;
			if (remainingDuration < 0f)
			{
				RemoveEffects();
			}
		}
	}

	private void OnUse()
	{
		if(remainingCooldown > 0f)
		{
			return;
		}

		remainingCooldown = cooldown;
		remainingDuration = duration;
		item.ItemUser.LandfillAnimator.PlayAttackAnimation();
		ApplyEffects();
	}

	private void ApplyEffects()
	{
		item.ItemUser.Movement.WalkingSpeed = walkSpeedBuff;
		item.ItemUser.Movement.SprintSpeed = sprintSpeedBuff;
		effectsApplied = true;
	}

	private void RemoveEffects()
	{
		item.ItemUser.Movement.WalkingSpeed = defaultWalkSpeed;
		item.ItemUser.Movement.SprintSpeed = defaultSprintSpeed;
		effectsApplied = false;
	}

}