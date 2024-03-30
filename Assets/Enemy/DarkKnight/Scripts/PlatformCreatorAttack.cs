using System.Collections.Generic;
using UnityEngine;

public class PlatformCreatorAttack : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;
    [SerializeField] private Destination endDestination;

    [Header("Platform Configurations")]
    [SerializeField] private ObstacleObjectPool hallwayObjectPool;
    [SerializeField] private ObstacleObjectPool stairsObjectPool;
    [SerializeField] private ObstacleObjectPool dropdownObjectPool;

    [Header("Other")]
    [SerializeField] private AudioClip swingAudioClip;

    private readonly Dictionary<PlatformType, ObstacleObjectPool> platforms = new();

    private enum PlatformType
    {
        Hallway,
        Stairs,
        Dropdown,
    }

    private void Awake()
    {
        platforms.Add(PlatformType.Hallway, hallwayObjectPool);
        platforms.Add(PlatformType.Stairs, stairsObjectPool);
        platforms.Add(PlatformType.Dropdown, dropdownObjectPool);
    }

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;
        OnEndAbility += EndAbility;
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
        PlatformType platformType = ChooseRandomPlatform();
        Vector2 spawnPosition = SpawnerInfo.Instance.SpawnerPositions[(int)SpawnPosition.Bot].position;

        if (platforms.TryGetValue(platformType, out ObstacleObjectPool platformObjectPool))
        {
            platformObjectPool.Pool.Get(out Obstacle obstacle);
            obstacle.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError($"There is no platform of type: {platformType}");
        }

        AudioManager.Instance.PlaySFX(swingAudioClip);
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        enemy.EnemyAnimationController.SetAnimatorTrigger(EnemyAnimatorParameter.Attack);
    }

    private PlatformType ChooseRandomPlatform()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(PlatformType)).Length);
        return (PlatformType)randomIndex;
    }

    private void EndAbility()
    {
        Vector2 destinationPosition = BossArena.Instance.GetDestination(endDestination);
        enemy.EnemyMovement.SetDestination(destinationPosition);
    }
}
