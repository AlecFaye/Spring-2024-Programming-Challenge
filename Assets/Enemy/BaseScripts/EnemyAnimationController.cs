using UnityEngine;

public enum EnemyAnimatorParameter
{
    IsMoving,
    Attack,
    AttackNoEffect,
    Die,

    ChargeUp,
    RollAttack,
    FinishRollAttack,
    FinishMiniAttack,
    ChargeUpHover,
    Hover,
    Slam,
    FinishSlam,
    
    FinishDeath,
}

public class EnemyAnimationController : MonoBehaviour
{
    public delegate void EnemyAnimationEvent();
    public EnemyAnimationEvent OnTriggerAbility;
    public EnemyAnimationEvent OnEndAbility;
    public EnemyAnimationEvent OnFinishDeath;

    [SerializeField] protected Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform visualManagerTF;

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

    public void FinishDeath()
    {
        OnFinishDeath?.Invoke();
    }

    private void Enemy_OnDie()
    {
        SetAnimatorTrigger(EnemyAnimatorParameter.Die);
    }

    public void Flip()
    {
        Vector3 visualScale = visualManagerTF.localScale;
        visualScale.x *= -1;
        visualManagerTF.localScale = visualScale;
    }
}
