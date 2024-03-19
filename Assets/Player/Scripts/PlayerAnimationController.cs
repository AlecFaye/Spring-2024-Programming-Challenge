using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private enum PlayerAnimatorParameter
    {
        IsRunning,
        Jump,
        VerticalVelocity
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.PlayerMovement.OnJumped += Player_OnJumped;
    }

    private void FixedUpdate()
    {
        HandleMovementAnimation();
    }

    private void Player_OnJumped()
    {
        animator.SetTrigger(PlayerAnimatorParameter.Jump.ToString());
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
