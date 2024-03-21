using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public delegate void PlayerAttackEvent();
    public PlayerAttackEvent OnAttackStarted;
    public PlayerAttackEvent OnAttackReleased;

    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Transform releaseAttackLocationTF;
    [SerializeField] private float attackCooldown = 0.5f;

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
        Instantiate(attackPrefab, releaseAttackLocationTF.position, Quaternion.identity);
    }

    private IEnumerator StartAttack()
    {
        canAttack = false;
        OnAttackStarted?.Invoke();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
