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

    [SerializeField] private float startSpawnDelay = 5.0f;
    [SerializeField] private float obstacleSpawnDelay = 5.0f;
    [SerializeField] private float bossDelay = 30.0f;

    private Transform[] spawnerPositions;
    public Transform[] SpawnerPositions => spawnerPositions;

    private float time = 0.0f;
    private float timeBossWasDefeated = 0.0f;
    private bool isFightingBoss = false;

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
        StartCoroutine(StartObstacleSpawn());
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeBossWasDefeated + bossDelay && !isFightingBoss)
        {
            StopAllCoroutines();

            EnemyType bossType = ChooseBoss();
            SpawnBoss(bossType);

            isFightingBoss = true;
        }
    }

    private IEnumerator StartObstacleSpawn()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        while (true)
        {
            ObstacleInfo obstacleInfo = ChooseObstacle();
            SpawnObstacle(obstacleInfo);

            yield return new WaitForSeconds(obstacleSpawnDelay);
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

    private EnemyType ChooseBoss()
    {
        int randomIndex = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);

        return (EnemyType)randomIndex;
    }

    public void Spawn_BossDefeated()
    {
        isFightingBoss = false;

        timeBossWasDefeated = time;

        StartCoroutine(StartObstacleSpawn());
    }

    private void InitSpawnPositions()
    {
        spawnerPositions = new Transform[spawnPositionsTF.childCount];

        for (int index = 0; index < spawnPositionsTF.childCount; index++)
            spawnerPositions[index] = spawnPositionsTF.GetChild(index).transform;
    }
}
