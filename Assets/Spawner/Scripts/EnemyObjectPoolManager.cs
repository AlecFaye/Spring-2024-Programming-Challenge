using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    FireMage,
    Mushroom,
    DarkKnight,
}

public class EnemyObjectPoolManager : MonoBehaviour
{
    public static EnemyObjectPoolManager Instance { get; private set; }

    [SerializeField] private EnemyInfo[] enemies;

    private readonly Dictionary<EnemyType, EnemyObjectPool> enemySpawners = new();

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

            EnemyObjectPool obstacleSpawner = enemyParent.transform.AddComponent<EnemyObjectPool>();
            obstacleSpawner.EnemyPrefab = enemyInfo.Enemy;
            obstacleSpawner.transform.parent = enemyParent.transform;

            enemySpawners.Add(enemyInfo.Type, obstacleSpawner);
        }
    }

    public EnemyObjectPool GetEnemySpawner(EnemyType type)
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
