using System.Collections;
using UnityEngine;

public class MiniMushroomJumpers : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;

    [Header("Mini Mushroom Configurations")]
    [SerializeField] private Destination destination;
    [SerializeField] private SpawnPosition spawnIndex;
    [SerializeField] private int numberOfMushroomsToSpawn = 1;
    [SerializeField] private float spawnDelay = 0.25f;

    [Header("Hard Mode Configurations")]
    [SerializeField] private int hardModeNumberOfMushroomsToSpawn = 10;

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

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;
        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.ChargeUp);

        StartCoroutine(SpawnMiniMushrooms());
    }

    private IEnumerator SpawnMiniMushrooms()
    {
        WaitForSeconds wait = new(spawnDelay);

        Transform spawnTF = SpawnerInfo.Instance.SpawnerPositions[(int)spawnIndex];
        ObstacleObjectPool obstacleSpawner = ObstacleObjectPoolManager.Instance.GetObstacleSpawner(ObstacleType.MiniMushroom);

        int adjustedSpawn = enemy.EnemyAI.IsHardModeOn
            ? hardModeNumberOfMushroomsToSpawn
            : numberOfMushroomsToSpawn;

        for (int i = 0; i < adjustedSpawn; i++)
        {
            obstacleSpawner.Pool.Get(out Obstacle obstacle);
            obstacle.transform.position = spawnTF.position;

            yield return wait;
        }

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.FinishMiniAttack);
    }
}
