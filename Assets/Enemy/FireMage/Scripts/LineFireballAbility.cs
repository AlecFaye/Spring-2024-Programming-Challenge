using System.Collections;
using UnityEngine;

public class LineFireballAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Line Fireball Configurations")]
    [SerializeField] private ProjectileSpawner fireballSpawner;
    [SerializeField] private SpawnPosition[] spawnIndices;
    [SerializeField] private int numberOfFireballs = 1;
    [SerializeField] private float delayBetweenFireballs;

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
        StartCoroutine(StartFireball());
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        foreach (SpawnPosition spawnIndex in spawnIndices)
            DangerIndicatorManager.Instance.DisplayIndicator(spawnIndex);
    }

    private IEnumerator StartFireball()
    {
        WaitForSeconds wait = new(delayBetweenFireballs);

        for (int i = 0; i < numberOfFireballs; i++)
        {
            foreach (SpawnPosition spawnIndex in spawnIndices)
            {
                Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndex];

                fireballSpawner.Pool.Get(out Projectile projectile);
                projectile.transform.position = spawnTF.position;
            }
            yield return wait;
        }
    }
}
