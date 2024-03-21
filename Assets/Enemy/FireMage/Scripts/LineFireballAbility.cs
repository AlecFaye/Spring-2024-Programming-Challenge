using System.Collections;
using UnityEngine;

public class LineFireballAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform destinationTF;

    [Header("Line Fireball Configurations")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private SpawnPosition spawnIndex;
    [SerializeField] private int numberOfFireballs = 1;
    [SerializeField] private float delayBetweenFireballs;

    [Header("Other")]
    [SerializeField] private EnemyAnimatorParameter animationToPlayParameter;

    private void Start()
    {
        enemy.EnemyMovement.OnReachedDestination += Enemy_OnReachedDestination;

        OnTriggerAbility += TriggerAbility;
    }

    public override void StartAbility()
    {
        base.StartAbility();

        enemy.EnemyMovement.SetDestination(destinationTF.position);
    }

    public override void TriggerAbility()
    {
        StartCoroutine(StartFireball());
    }

    private void Enemy_OnReachedDestination()
    {
        enemy.EnemyAnimationController.SetAnimatorTrigger(animationToPlayParameter);
    }

    private IEnumerator StartFireball()
    {
        WaitForSeconds wait = new(delayBetweenFireballs);

        Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndex];

        for (int i = 0; i < numberOfFireballs; i++)
        {
            Instantiate(fireballPrefab, spawnTF.position, Quaternion.identity);

            yield return wait;
        }
    }
}
