using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ObstacleType
{
    Health,
    MiniMushroom,
    Shield,
    Wall_1000,
    Wall_1001,
    Wall_1011,
    Wall_1100,
    Wall_1101,
    Wall_1110,
}

public class ObstacleObjectPoolManager : MonoBehaviour
{
    public static ObstacleObjectPoolManager Instance { get; private set; }

    [SerializeField] private ObstacleInfo[] obstacles;

    private readonly Dictionary<ObstacleType, ObstacleObjectPool> obstacleSpawners = new();

    [System.Serializable]
    private struct ObstacleInfo
    {
        public ObstacleType Type;
        public Obstacle Obstacle;
    }

    private void Awake()
    {
        InitObstacleSpawners();

        Instance = this;
    }

    private void InitObstacleSpawners()
    {
        foreach (ObstacleInfo obstacleInfo in obstacles)
        {
            GameObject obstacleParent = new($"{obstacleInfo.Obstacle} (Obstacle Spawner)");
            obstacleParent.transform.parent = transform;

            ObstacleObjectPool obstacleSpawner = obstacleParent.transform.AddComponent<ObstacleObjectPool>();
            obstacleSpawner.ObstaclePrefab = obstacleInfo.Obstacle;
            obstacleSpawner.transform.parent = obstacleParent.transform;

            obstacleSpawners.Add(obstacleInfo.Type, obstacleSpawner);
        }
    }

    public ObstacleObjectPool GetObstacleSpawner(ObstacleType type)
    {
        if (obstacleSpawners.ContainsKey(type))
            return obstacleSpawners[type];
        else
        {
            Debug.LogError($"No Obstacle Spawner of Type: {type}");
            return null;
        }
    }
}
