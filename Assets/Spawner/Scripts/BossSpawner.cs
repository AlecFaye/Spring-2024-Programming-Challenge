using System.Collections;
using System.Collections.Generic;
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

    private List<EnemyType> undefeatedBosses = new();

    public bool IsFightingBoss { get; private set; }

    private EnemyType bossType;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Boss Spawner in the scene.");

        Instance = this;

        InitBossDefeatedDictionary();
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
        bossType = ChooseBoss();

        yield return new WaitForSeconds(startSpawnDelay);

        EnemyObjectPool enemySpawner = EnemyObjectPoolManager.Instance.GetEnemySpawner(bossType);
        enemySpawner.Pool.Get(out Enemy enemy);

        Vector2 spawnPosition = spawnerPositions[(int)SpawnPosition.Bot].position;
        enemy.transform.position = spawnPosition;
    }

    private EnemyType ChooseBoss()
    {
        if (undefeatedBosses.Count > 0)
        {
            int randomIndex = Random.Range(0, undefeatedBosses.Count);
            return undefeatedBosses[randomIndex];
        }
        else
        {
            int randomIndex = Random.Range(0, bossSpawns.Length);
            return bossSpawns[randomIndex];
        }
    }

    public void Spawn_BossDefeated()
    {
        IsFightingBoss = false;
        timeBossWasDefeated = time;
        OnDefeatedBoss?.Invoke();
        undefeatedBosses.Remove(bossType);
    }

    private void InitBossDefeatedDictionary()
    {
        for (int index = 0; index < System.Enum.GetValues(typeof(EnemyType)).Length; index++)
            undefeatedBosses.Add((EnemyType)index);
    }
}
