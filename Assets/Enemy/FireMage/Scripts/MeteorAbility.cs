using System.Collections;
using UnityEngine;

public class MeteorAbility : EnemyAbility
{
    [Header("References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private Destination destination;

    [Header("Meteor Configurations")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private SpawnPosition[] spawnIndices;
    [SerializeField] private int numberOfMeteors = 1;
    [SerializeField] private float delayBetweenMeteors;

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
    }

    private IEnumerator StartFireball()
    {
        WaitForSeconds wait = new(delayBetweenMeteors);

        for (int i = 0; i < numberOfMeteors; i++)
        {
            int randomIndex = Random.Range(0, spawnIndices.Length);

            Transform spawnTF = Spawner.Instance.SpawnerPositions[(int)spawnIndices[randomIndex]];

            Instantiate(meteorPrefab, spawnTF.position, Quaternion.identity);

            yield return wait;
        }
    }
}
