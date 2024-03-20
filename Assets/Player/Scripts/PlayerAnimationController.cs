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
        Revive
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.PlayerMovement.OnJumped += Player_OnJumped;

        Player.Instance.PlayerStats.HealthSystem.OnDie += Player_OnDie;
        Player.Instance.PlayerStats.HealthSystem.OnRevive += Player_OnRevive;
    }

    private void FixedUpdate()
    {
        if (Player.Instance.PlayerStats.IsDead)
            return;

        HandleMovementAnimation();
    }

    private void Player_OnJumped()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Jump.ToString());
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
