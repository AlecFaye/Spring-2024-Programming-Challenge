using UnityEngine;

public class FirestormAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Firestorm Configurations")]
    [SerializeField] private ProjectileSpawner firestormSpawner;
    [SerializeField] private SpawnPosition spawnIndex;
    [SerializeField] private SpawnPosition[] dangerIndicatorsSpawns;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;

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
        Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndex];

        firestormSpawner.Pool.Get(out Projectile projectile);
        projectile.transform.position = spawnTF.position;
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        foreach (SpawnPosition spawnIndex in dangerIndicatorsSpawns)
            DangerIndicatorManager.Instance.DisplayIndicator(spawnIndex);
    }
}
