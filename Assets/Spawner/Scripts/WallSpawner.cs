using System.Collections;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleInfo[] wallObstacles;

    [SerializeField] private float startSpawnDelay = 5.0f;
    [SerializeField] private float obstacleSpawnDelay = 5.0f;

    private Transform[] spawnerPositions;

    [System.Serializable]
    private struct ObstacleInfo
    {
        public ObstacleType ObstacleType;
        public SpawnPosition SpawnPosition;
    }

    private void Start()
    {
        spawnerPositions = SpawnerInfo.Instance.SpawnerPositions;

        StartCoroutine(StartObstacleSpawn());

        BossSpawner.Instance.OnSpawnedBoss += Boss_OnSpawned;
        BossSpawner.Instance.OnDefeatedBoss += Boss_OnDefeated;
    }

    private IEnumerator StartObstacleSpawn()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        while (true)
        {
            ObstacleInfo obstacleInfo = ChooseRandomWallObstacle();
            SpawnObstacle(obstacleInfo);

            yield return new WaitForSeconds(obstacleSpawnDelay);
        }
    }

    private void SpawnObstacle(ObstacleInfo obstacleInfo)
    {
        ObstacleObjectPool obstacleSpawner = ObstacleObjectPoolManager.Instance.GetObstacleSpawner(obstacleInfo.ObstacleType);
        obstacleSpawner.Pool.Get(out Obstacle mover);

        int spawnIndex = (int)obstacleInfo.SpawnPosition;
        mover.transform.position = spawnerPositions[spawnIndex].position;
    }

    private ObstacleInfo ChooseRandomWallObstacle()
    {
        int randomIndex = Random.Range(0, wallObstacles.Length);
        return wallObstacles[randomIndex];
    }

    private void Boss_OnSpawned()
    {
        StopAllCoroutines();
    }

    private void Boss_OnDefeated()
    {
        StartCoroutine(StartObstacleSpawn());
    }
}
