using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private enum PlayerAnimatorParameter
    {
        IsRunning,
        Jump,
        VerticalVelocity,
        Die,
        Revive,
        Dash,
        FinishedDash,
        Attack,
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.PlayerMovement.OnJumped += Player_OnJumped;
        Player.Instance.PlayerMovement.OnDashed += Player_OnDashed;
        Player.Instance.PlayerMovement.OnDashFinished += Player_OnDashFinished;

        Player.Instance.PlayerAttack.OnAttackStarted += Player_OnAttackStarted;

        Player.Instance.PlayerStats.HealthSystem.OnDie += Player_OnDie;
        Player.Instance.PlayerStats.HealthSystem.OnRevive += Player_OnRevive;
    }

    private void FixedUpdate()
    {
        if (Player.Instance.PlayerStats.IsDead || Player.Instance.PlayerAttack.IsAttacking)
            return;

        HandleMovementAnimation();
    }

    public void ReleaseAttack()
    {
        Player.Instance.PlayerAttack.OnAttackReleased?.Invoke();
    }

    public void FinishedDeath()
    {
        Time.timeScale = 0.0f;
    }

    private void Player_OnJumped()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Jump.ToString());
    }

    private void Player_OnDashed()
    {
        animator.ResetTrigger(PlayerAnimatorParameter.FinishedDash.ToString());
        animator.SetTrigger(PlayerAnimatorParameter.Dash.ToString());
    }

    private void Player_OnDashFinished()
    {
        animator.SetTrigger(PlayerAnimatorParameter.FinishedDash.ToString());
    }

    private void Player_OnAttackStarted()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Attack.ToString());
    }

    private void Player_OnDie()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Die.ToString());
    }

    private void Player_OnRevive()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Revive.ToString());
    }

    private void HandleMovementAnimation()
    {
        if (Player.Instance.PlayerMovement.IsGrounded)
        {
            animator.SetFloat(PlayerAnimatorParameter.VerticalVelocity.ToString(), 0.0f);
        }
        else
        {
            animator.SetFloat(PlayerAnimatorParameter.VerticalVelocity.ToString(), Player.Instance.PlayerMovement.FrameVelocity.y);
        }
    }
}
