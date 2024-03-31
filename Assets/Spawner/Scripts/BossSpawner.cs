using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public static BossSpawner Instance { get; private set; }

    public delegate void BossSpawnerEvent();
    public BossSpawnerEvent OnSpawnedBoss;
    public BossSpawnerEvent OnDefeatedBoss;

    [SerializeField] private EnemyType[] bossSpawns;
    [SerializeField] private float startSpawnDelay = 5.0f;
    [SerializeField] private float bossDelay = 30.0f;

    private float time = 0.0f;
    private float timeBossWasDefeated = 0.0f;

    private Transform[] spawnerPositions;
    private bool isFinishedTutorial = false;

    public bool IsFightingBoss { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Boss Spawner in the scene.");

        Instance = this;
    }

    private void Start()
    {
        spawnerPositions = SpawnerInfo.Instance.SpawnerPositions;

        TutorialUI.Instance.OnCompletedTutorial += StartSpawn;
    }

    private void Update()
    {
        if (isFinishedTutorial)
            time += Time.deltaTime;

        if (time >= timeBossWasDefeated + bossDelay && !IsFightingBoss && isFinishedTutorial)
        {
            StartCoroutine(SpawnBoss());
            OnSpawnedBoss?.Invoke();
            IsFightingBoss = true;
        }
    }

    private void StartSpawn()
    {
        isFinishedTutorial = true;
    }

    private IEnumerator SpawnBoss()
    {
        EnemyType bossType = ChooseBoss();

        yield return new WaitForSeconds(startSpawnDelay);

        EnemyObjectPool enemySpawner = EnemyObjectPoolManager.Instance.GetEnemySpawner(bossType);
        enemySpawner.Pool.Get(out Enemy enemy);

        Vector2 spawnPosition = spawnerPositions[(int)SpawnPosition.Bot].position;
        enemy.transform.position = spawnPosition;
    }

    private EnemyType ChooseBoss()
    {
        int randomIndex = Random.Range(0, bossSpawns.Length);
        return bossSpawns[randomIndex];
    }

    public void Spawn_BossDefeated()
    {
        IsFightingBoss = false;
        timeBossWasDefeated = time;
        OnDefeatedBoss?.Invoke();
    }
}
