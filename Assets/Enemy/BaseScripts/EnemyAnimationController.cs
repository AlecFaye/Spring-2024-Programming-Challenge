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
    public delegate void EnemyAnimationEvent();
    public EnemyAnimationEvent OnTriggerAbility;
    public EnemyAnimationEvent OnEndAbility;

    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    private void Start()
    {
        enemy.EnemyStats.HealthSystem.OnDie += Enemy_OnDie;
    }

    public void SetAnimatorTrigger(EnemyAnimatorParameter parameter)
    {
        animator.SetTrigger(parameter.ToString());
    }

    public void SetAnimatorBool(EnemyAnimatorParameter parameter, bool status)
    {
        animator.SetBool(parameter.ToString(), status);
    }

    public void TriggerAbility()
    {
        OnTriggerAbility?.Invoke();
    }

    public void EndAbility()
    {
        OnEndAbility?.Invoke();
    }

    private void Enemy_OnDie()
    {
        SetAnimatorTrigger(EnemyAnimatorParameter.Die);
    }
}
