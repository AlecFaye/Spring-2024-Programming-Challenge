using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    private enum EnemyAnimatorParameter
    {
        IsMoving,
        Attack,
        AttackNoEffect,
        Die
    }

    private void Start()
    {
        enemy.EnemyStats.HealthSystem.OnDie += Enemy_OnDie;
    }

    private void Enemy_OnDie()
    {
        animator.SetTrigger(EnemyAnimatorParameter.Die.ToString());
    }
}
