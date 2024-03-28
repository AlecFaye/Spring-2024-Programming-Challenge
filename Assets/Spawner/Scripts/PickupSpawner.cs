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
            ObstacleType randomObstacle = ChooseObstacle();
            ObstacleSpawner obstacleSpawner = ObstacleSpawnerManager.Instance.GetObstacleSpawner(randomObstacle);
            obstacleSpawner.Pool.Get(out Obstacle obstacle);

            int randomIndex = Random.Range(0, pickupSpawnPositions.Length);
            Vector3 randomPosition = spawnerPositions[randomIndex].position;
            obstacle.transform.position = randomPosition;

            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private ObstacleType ChooseObstacle()
    {
        int randomIndex = Random.Range(0, pickupObstacles.Length);

        return pickupObstacles[randomIndex];
    }
}
