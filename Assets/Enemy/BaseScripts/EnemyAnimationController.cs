using UnityEngine;

public enum EnemyAnimatorParameter
{
    IsMoving,
    Attack,
    AttackNoEffect,
    Die
}

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAbility ability;

    private void Start()
    {
        enemy.EnemyStats.HealthSystem.OnDie += Enemy_OnDie;
    }

    public void SetTriggerAnimator(EnemyAnimatorParameter parameter)
    {
        animator.SetTrigger(parameter.ToString());
    }

    public void TriggerAbility()
    {
        ability.OnTriggerAbility?.Invoke();
    }

    public void EndAbility()
    {
        ability.OnEndAbility?.Invoke();
    }

    private void Enemy_OnDie()
    {
        SetTriggerAnimator(EnemyAnimatorParameter.Die);
    }
}
