using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public delegate void PlayerAttackEvent();
    public PlayerAttackEvent OnAttackStarted;
    public PlayerAttackEvent OnAttackReleased;

    [SerializeField] private ProjectileObjectPool projectileSpawner;
    [SerializeField] private Transform releaseAttackLocationTF;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private AudioClip attackAudioClip;

    public bool IsAttacking => !canAttack;
    private bool canAttack = true;

    private void Start()
    {
        OnAttackReleased += Player_OnAttackReleased;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && canAttack)
        {
            StartCoroutine(StartAttack());
        }
    }

    private void Player_OnAttackReleased()
    {
        projectileSpawner.Pool.Get(out Projectile projectile);

        projectile.transform.position = releaseAttackLocationTF.position;
    }

    private IEnumerator StartAttack()
    {
        canAttack = false;
        OnAttackStarted?.Invoke();
        AudioManager.Instance.PlaySFX(attackAudioClip);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
