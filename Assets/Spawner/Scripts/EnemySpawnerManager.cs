using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    FireMage,
    Mushroom,
}

public class EnemySpawnerManager : MonoBehaviour
{
    public static EnemySpawnerManager Instance { get; private set; }

    [SerializeField] private EnemyInfo[] enemies;

    private readonly Dictionary<EnemyType, EnemySpawner> enemySpawners = new();

    [System.Serializable]
    private struct EnemyInfo
    {
        public EnemyType Type;
        public Enemy Enemy;
    }

    private void Awake()
    {
        InitEnemySpawners();

        Instance = this;
    }

    private void InitEnemySpawners()
    {
        foreach (EnemyInfo enemyInfo in enemies)
        {
            GameObject enemyParent = new($"{enemyInfo.Enemy} (Obstacle Spawner)");
            enemyParent.transform.parent = transform;

            EnemySpawner obstacleSpawner = enemyParent.transform.AddComponent<EnemySpawner>();
            obstacleSpawner.EnemyPrefab = enemyInfo.Enemy;
            obstacleSpawner.transform.parent = enemyParent.transform;

            enemySpawners.Add(enemyInfo.Type, obstacleSpawner);
        }
    }

    public EnemySpawner GetEnemySpawner(EnemyType type)
    {
        if (enemySpawners.ContainsKey(type))
            return enemySpawners[type];
        else
        {
            Debug.LogError($"No Enemy Spawner of Type: {type}");
            return null;
        }
    }
}
