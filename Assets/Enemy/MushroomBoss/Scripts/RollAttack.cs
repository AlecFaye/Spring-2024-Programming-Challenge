using UnityEngine;

public class RollAttack : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Collider2D damageCollider;

    [Header("Roll Attack Configurations")]
    [SerializeField] private Destination startDestination;
    [SerializeField] private Destination rollAttackDestination;
    [SerializeField] private Destination originalDestination;

    [SerializeField] private SpawnPosition[] dangerPositions;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;

        damageCollider.enabled = false;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        Vector2 destinationPosition = BossArena.Instance.GetDestination(startDestination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedDestination;
    }

    public override void TriggerAbility()
    {
        TriggerFirstPart();
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.RollAttack);
    }

    private void TriggerFirstPart()
    {
        Vector2 destinationPosition = BossArena.Instance.GetDestination(rollAttackDestination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_ReachedFirstDestination;

        damageCollider.enabled = true;

        foreach (SpawnPosition spawnIndex in dangerPositions)
            DangerIndicatorManager.Instance.DisplayIndicator(spawnIndex);
    }

    private void Enemy_ReachedFirstDestination()
    {
        enemy.EnemyAnimationController.Flip();

        enemy.EnemyMovement.OnReachedDestination -= Enemy_ReachedFirstDestination;

        Vector2 destinationPosition = BossArena.Instance.GetDestination(originalDestination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_ReachedSecondDestination;
    }

    private void Enemy_ReachedSecondDestination()
    {
        enemy.EnemyAnimationController.Flip();

        enemy.EnemyMovement.OnReachedDestination -= Enemy_ReachedSecondDestination;
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.FinishRollAttack);

        damageCollider.enabled = false;
    }
}
