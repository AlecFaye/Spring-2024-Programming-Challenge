using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public delegate void PlayerMovementEvent();
    public PlayerMovementEvent OnJumped;
    public PlayerMovementEvent OnLanded;
    public PlayerMovementEvent OnDashed;
    public PlayerMovementEvent OnDashFinished;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Ground Configurations")]
    [SerializeField] private float groundingForce = -1.5f;
    [SerializeField] private float grounderDistance = 0.05f;

    [Header("Jump Configurations")]
    [SerializeField] private float jumpPower = 36.0f;
    [SerializeField] private float maxFallSpeed = 40.0f;
    [SerializeField] private float fallAcceleration = 110.0f;
    [SerializeField] private float jumpEndEarlyGravityModifier = 3.0f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBuffer = 0.2f;

    [Header("Dash Configurations")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    public bool IsGrounded => isGrounded;
    public Vector2 FrameVelocity => frameVelocity;

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    private Vector2 frameVelocity;

    private bool cachedQueryStartInColliders;

    private float time;

    private float frameLeftGrounded = float.MinValue;
    private bool isGrounded;

    private bool bufferedJumpUsable;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private bool jumpToConsume;
    private float timeJumpWasPressed;

    private bool jump;

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + jumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !isGrounded && time < frameLeftGrounded + coyoteTime;

    private bool isDashing;
    private bool canDash = true;

    #region Pipeline Functions

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        if (!isDashing)
        {
            HandleJump();
            HandleGravity();
        }

        rb.velocity = frameVelocity;
    }

    #endregion

    #region Input Functions

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump = true;

            jumpToConsume = true;
            timeJumpWasPressed = time;
        }
        else if (context.canceled)
        {
            jump = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash && !Player.Instance.PlayerAttack.IsAttacking)
        {
            StartCoroutine(Dash());
        }  
    }

    #endregion

    #region Helper Functions

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        bool isGroundHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.size, capsuleCollider.direction, 0, Vector2.down, grounderDistance, ~playerLayerMask);
        bool isCeilingHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.size, capsuleCollider.direction, 0, Vector2.up, grounderDistance, ~playerLayerMask);

        if (isCeilingHit)
        {
            frameVelocity.y = Mathf.Min(0, frameVelocity.y);
        }

        if (!isGrounded && isGroundHit)
        {
            isGrounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
            OnLanded?.Invoke();
        }
        else if (isGrounded && !isGroundHit)
        {
            isGrounded = false;
            frameLeftGrounded = time;
        }

        Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
    }

    private void HandleJump()
    {
        if (!endedJumpEarly && !isGrounded && !jump && rb.velocity.y > 0)
            endedJumpEarly = true;

        if (!jumpToConsume && !HasBufferedJump)
            return;

        if (isGrounded || CanUseCoyote)
            ExecuteJump();

        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0.0f;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = jumpPower;
        OnJumped?.Invoke();
    }

    private void HandleGravity()
    {
        if (isGrounded && frameVelocity.y <= 0.0f)
        {
            frameVelocity.y = groundingForce;
        }
        else
        {
            float inAirGravity = fallAcceleration;

            if (endedJumpEarly && frameVelocity.y > 0.0f)
                inAirGravity *= jumpEndEarlyGravityModifier;

            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        frameVelocity.y = 0.0f;
        OnDashed?.Invoke();

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        OnDashFinished?.Invoke();

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    #endregion
}
