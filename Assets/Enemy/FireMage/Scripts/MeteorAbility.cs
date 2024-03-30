using System.Collections;
using UnityEngine;

public class MeteorAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Meteor Configurations")]
    [SerializeField] private ProjectileObjectPool meteorSpawner;
    [SerializeField] private SpawnPosition[] spawnIndices;
    [SerializeField] private int numberOfMeteors = 1;
    [SerializeField] private float delayBetweenMeteors;

    [Header("Hard Mode Configurations")]
    [SerializeField] private int hardModeNumberOfMeteors = 1;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;
    [SerializeField] private AudioClip meteorAudioClip;

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
        AudioManager.Instance.PlaySFX(meteorAudioClip);

        StartCoroutine(StartMeteors());
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        foreach (SpawnPosition spawnIndex in spawnIndices)
            DangerIndicatorManager.Instance.DisplayIndicator(spawnIndex);
    }

    private IEnumerator StartMeteors()
    {
        WaitForSeconds wait = new(delayBetweenMeteors);

        int adjustedNumberOfMeteors = enemy.EnemyAI.IsHardModeOn
            ? hardModeNumberOfMeteors
            : numberOfMeteors;

        for (int i = 0; i < adjustedNumberOfMeteors; i++)
        {
            int randomIndex = Random.Range(0, spawnIndices.Length);

            Transform spawnTF = SpawnerInfo.Instance.SpawnerPositions[(int)spawnIndices[randomIndex]];

            meteorSpawner.Pool.Get(out Projectile projectile);
            projectile.transform.position = spawnTF.position;

            yield return wait;
        }
    }
}
