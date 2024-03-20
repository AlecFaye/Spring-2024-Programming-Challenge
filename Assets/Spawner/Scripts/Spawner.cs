using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPositionsTF;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private WaveScriptableObject[] waveSOs;

    private Transform[] spawnerPositions;
    private bool isFightingBoss = false;

    private void Awake()
    {
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

    private void Spawn_BossDefeated()
    {
        isFightingBoss = false;
    }
}
