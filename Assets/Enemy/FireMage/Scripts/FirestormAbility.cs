using UnityEngine;

public class FirestormAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform destinationTF;

    [Header("Firestorm Configurations")]
    [SerializeField] private GameObject firestormPrefab;
    [SerializeField] private SpawnPosition spawnIndex;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;

    private void Start()
    {
        OnTriggerAbility += TriggerAbility;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        enemy.EnemyMovement.SetDestination(destinationTF.position);
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedDestination;
    }

    public override void TriggerAbility()
    {
        Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndex];

        Instantiate(firestormPrefab, spawnTF.position, Quaternion.identity);
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
        enemy.EnemyMovement.OnReachedDestination -= Enemy_OnReachedDestination;
    }
}
