using System.Collections;
using UnityEngine;

public enum SpawnPosition
{
    Top,
    TopMid,
    BotMid,
    Bot,
    LeftTop,
    RightTop
}

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private Transform spawnPositionsTF;
    [SerializeField] private ObstacleInfo[] wallObstacles;
    [SerializeField] private ObstacleType[] pickupObstacles;
    [SerializeField] private float spawnDelay = 5.0f;

    private Transform[] spawnerPositions;
    public Transform[] SpawnerPositions => spawnerPositions;

    private float time = 0.0f;

    [System.Serializable]
    private struct ObstacleInfo
    {
        public ObstacleType ObstacleType;
        public SpawnPosition SpawnPosition;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Spawner in the scene.");

        Instance = this;

        InitSpawnPositions();
    }

    private void Start()
    {
        //StartCoroutine(StartObstacleSpawn());

        SpawnBoss(EnemyType.Mushroom);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator StartObstacleSpawn()
    {
        while (true)
        {
            ObstacleInfo obstacleInfo = ChooseObstacle();
            SpawnObstacle(obstacleInfo);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnObstacle(ObstacleInfo obstacleInfo)
    {
        ObstacleSpawner obstacleSpawner = ObstacleSpawnerManager.Instance.GetObstacleSpawner(obstacleInfo.ObstacleType);
        obstacleSpawner.Pool.Get(out Obstacle mover);

        int spawnIndex = (int)obstacleInfo.SpawnPosition;
        mover.transform.position = spawnerPositions[spawnIndex].position;
    }

    private void SpawnBoss(EnemyType enemyType)
    {
        EnemySpawner enemySpawner = EnemySpawnerManager.Instance.GetEnemySpawner(enemyType);
        enemySpawner.Pool.Get(out Enemy enemy);

        Vector2 spawnPosition = spawnerPositions[(int)SpawnPosition.Bot].position;
        enemy.transform.position = spawnPosition;
    }

    private ObstacleInfo ChooseObstacle()
    {
        int randomIndex = Random.Range(0, wallObstacles.Length);

        return wallObstacles[randomIndex];
    }

    public void Spawn_BossDefeated()
    {
        StartCoroutine(StartObstacleSpawn());
    }

    private void InitSpawnPositions()
    {
        spawnerPositions = new Transform[spawnPositionsTF.childCount];

        for (int index = 0; index < spawnPositionsTF.childCount; index++)
            spawnerPositions[index] = spawnPositionsTF.GetChild(index).transform;
    }
}
