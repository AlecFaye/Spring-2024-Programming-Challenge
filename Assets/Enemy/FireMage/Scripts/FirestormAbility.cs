using UnityEngine;

public class FirestormAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Firestorm Configurations")]
    [SerializeField] private ProjectileObjectPool firestormSpawner;
    [SerializeField] private SpawnPosition spawnIndex;
    [SerializeField] private SpawnPosition[] dangerIndicatorsSpawns;
    [SerializeField] private float firestormSpeed = -10.0f;

    [Header("Hard Mode Configurations")]
    [SerializeField] private float hardModeFirestormSpeed = -20.0f;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;
    [SerializeField] private AudioClip firestormAudioClip;

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
        Transform spawnTF = SpawnerInfo.Instance.SpawnerPositions[(int)spawnIndex];

        firestormSpawner.Pool.Get(out Projectile projectile);
        projectile.transform.position = spawnTF.position;

        float adjustedSpeed = enemy.EnemyAI.IsHardModeOn
            ? hardModeFirestormSpeed
            : firestormSpeed;

        projectile.SetSpeed(adjustedSpeed);

        AudioManager.Instance.PlaySFX(firestormAudioClip);
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;

        foreach (SpawnPosition spawnIndex in dangerIndicatorsSpawns)
            DangerIndicatorManager.Instance.DisplayIndicator(spawnIndex);
    }
}
