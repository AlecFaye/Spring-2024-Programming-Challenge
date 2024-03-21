using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private Transform spawnPositionsTF;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private WaveScriptableObject[] waveSOs;

    private Transform[] spawnerPositions;
    private bool isFightingBoss = false;

    public Transform[] SpawnerPositions => spawnerPositions;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Spawner in the scene.");

        Instance = this;

        spawnerPositions = new Transform[spawnPositionsTF.childCount];

        for (int index = 0; index < spawnPositionsTF.childCount; index++)
            spawnerPositions[index] = spawnPositionsTF.GetChild(index).transform;
    }

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        foreach (WaveScriptableObject waveSO in waveSOs)
        {
            foreach (Wave wave in waveSO.Waves)
            {
                if (waveSO.IsBossWave)
                    SpawnBoss(wave);
                else
                    SpawnObstacles(wave);

                yield return new WaitForSeconds(wave.Delay);
            }

            if (waveSO.IsBossWave)
            {
                isFightingBoss = true;

                while (isFightingBoss)
                    yield return null;
            }

            yield return new WaitForSeconds(waveSO.WaveEndDelay);
        }
    }

    private void SpawnObstacles(Wave wave)
    {
        foreach (Spawn spawn in wave.Spawns)
        {
            GameObject spawnPrefab = spawn.SpawnPrefab;
            int spawnIndex = (int)spawn.SpawnPosition;

            Instantiate(spawnPrefab, spawnerPositions[spawnIndex].position, Quaternion.identity);
        }
    }

    private void SpawnBoss(Wave wave)
    {
        foreach (Spawn spawn in wave.Spawns)
        {
            GameObject spawnPrefab = spawn.SpawnPrefab;
            int spawnIndex = (int)spawn.SpawnPosition;

            GameObject spawnedObject = Instantiate(spawnPrefab, spawnerPositions[spawnIndex].position, Quaternion.identity);
            if (spawnedObject.TryGetComponent(out Enemy enemy))
                enemy.EnemyStats.HealthSystem.OnDie += Spawn_BossDefeated;
        }
    }

    private void Spawn_BossDefeated()
    {
        isFightingBoss = false;
    }
}
