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
    Revive,
}

public class EnemyAnimationController : MonoBehaviour
{
    public delegate void EnemyAnimationEvent();
    public EnemyAnimationEvent OnTriggerAbility;
    public EnemyAnimationEvent OnEndAbility;

    [SerializeField] protected Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform visualManagerTF;

    private void Start()
    {
        enemy.EnemyStats.HealthSystem.OnDie += Enemy_OnDie;
        enemy.EnemyStats.HealthSystem.OnRevive += Enemy_OnRevive;
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
        animator.ResetTrigger(EnemyAnimatorParameter.Revive.ToString());
        SetAnimatorTrigger(EnemyAnimatorParameter.Die);
    }

    private void Enemy_OnRevive()
    {
        SetAnimatorTrigger(EnemyAnimatorParameter.Revive);
    }

    public void Flip()
    {
        Vector3 visualScale = visualManagerTF.localScale;
        visualScale.x *= -1;
        visualManagerTF.localScale = visualScale;
    }
}
