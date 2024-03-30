using System.Collections;
using UnityEngine;

public class LineFireballAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Line Fireball Configurations")]
    [SerializeField] private ProjectileObjectPool fireballSpawner;
    [SerializeField] private SpawnPosition[] spawnIndices;
    [SerializeField] private int numberOfFireballs = 1;
    [SerializeField] private float fireballSpeed = -350;
    [SerializeField] private float delayBetweenFireballs;

    [Header("Hard Mode Configurations")]
    [SerializeField] private int hardModeNumberOfFireballs = 1;
    [SerializeField] private float hardModeFireballSpeed = -350;
    [SerializeField] private float hardModeDelayBetweenFireballs;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;
    [SerializeField] private AudioClip fireballAudioClip;

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
        AudioManager.Instance.PlaySFX(fireballAudioClip);

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
        float adjustedDelay = enemy.EnemyAI.IsHardModeOn
            ? hardModeDelayBetweenFireballs
            : delayBetweenFireballs;

        int adjustedFireballs = enemy.EnemyAI.IsHardModeOn
            ? hardModeNumberOfFireballs
            : numberOfFireballs;

        float adjustedFireballSpeed = enemy.EnemyAI.IsHardModeOn
            ? hardModeFireballSpeed
            : fireballSpeed;

        WaitForSeconds wait = new(adjustedDelay);

        for (int i = 0; i < adjustedFireballs; i++)
        {
            foreach (SpawnPosition spawnIndex in spawnIndices)
            {
                Transform spawnTF = SpawnerInfo.Instance.SpawnerPositions[(int)spawnIndex];

                fireballSpawner.Pool.Get(out Projectile projectile);
                projectile.transform.position = spawnTF.position;

                projectile.SetSpeed(adjustedFireballSpeed);
            }
            yield return wait;
        }
    }
}
