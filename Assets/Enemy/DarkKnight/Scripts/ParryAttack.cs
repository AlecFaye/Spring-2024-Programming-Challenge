using System.Collections;
using UnityEngine;

public class ParryAttack : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Parry Attack Configurations")]
    [SerializeField] private float parryDuration = 2.5f;
    [SerializeField] private ProjectileObjectPool projectileObjectPool;
    [SerializeField] private Destination parryProjectileDestination;
    [SerializeField] private ParryHitbox parryHitbox;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        Vector2 destinationPosition = BossArena.Instance.GetDestination(destination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedDestination;
    }

    public override void TriggerAbility()
    {
        Vector2 projectileSpawnPosition = BossArena.Instance.GetDestination(parryProjectileDestination);
        projectileObjectPool.Pool.Get(out Projectile projectile);
        projectile.transform.position = projectileSpawnPosition;
    }

    public void Parry()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.Block);
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        enemy.EnemyAnimationController.SetAnimatorBool(EnemyAnimatorParameter.BlockIdle, true);

        StartCoroutine(StartParry());
    }

    private IEnumerator StartParry()
    {
        float time = 0.0f;
        parryHitbox.EnableCollider();

        while (time < parryDuration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        parryHitbox.DisableCollider();
        enemy.EnemyAnimationController.EndAbility();
        enemy.EnemyAnimationController.SetAnimatorBool(EnemyAnimatorParameter.BlockIdle, false);
    }
}