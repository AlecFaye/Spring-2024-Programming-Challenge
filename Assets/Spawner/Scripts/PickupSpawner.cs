using System.Collections;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleType[] pickupObstacles;
    [SerializeField] private SpawnPosition[] pickupSpawnPositions;

    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    private Transform[] spawnerPositions;

    private void Start()
    {
        spawnerPositions = Spawner.Instance.SpawnerPositions;

        StartCoroutine(StartPickupSpawn());
    }

    private IEnumerator StartPickupSpawn()
    {
        yield return new WaitForSeconds(minSpawnDelay);

        while (true)
        {
            ObstacleType randomPickup = ChooseRandomPickup();
            ObstacleSpawner pickupSpawner = ObstacleSpawnerManager.Instance.GetObstacleSpawner(randomPickup);
            pickupSpawner.Pool.Get(out Obstacle pickup);

            int randomIndex = Random.Range(0, pickupSpawnPositions.Length);
            Vector3 randomPosition = spawnerPositions[randomIndex].position;
            pickup.transform.position = randomPosition;

            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private ObstacleType ChooseRandomPickup()
    {
        int randomIndex = Random.Range(0, pickupObstacles.Length);

        return pickupObstacles[randomIndex];
    }
}
