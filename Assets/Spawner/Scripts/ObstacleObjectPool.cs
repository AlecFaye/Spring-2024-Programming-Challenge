using UnityEngine;
using UnityEngine.Pool;

public class ObstacleObjectPool : MonoBehaviour
{
    public ObjectPool<Obstacle> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    public Obstacle ObstaclePrefab;
    
    private GameObject parent;

    private void Start()
    {
        parent = new($"{ObstaclePrefab.name} (Object Pool)");
        Pool = new(CreateObstacle, OnGetObstacleFromPool, OnReleaseObstacleToPool, OnDestroyObstacle, true, defaultCapacity, maxSize);
    }

    private Obstacle CreateObstacle()
    {
        Obstacle obstacleObject = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity, parent.transform);

        obstacleObject.SetPool(Pool);

        return obstacleObject;
    }

    private void OnGetObstacleFromPool(Obstacle mover)
    {
        mover.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        mover.gameObject.SetActive(true);
    }
    
    private void OnReleaseObstacleToPool(Obstacle mover)
    {
        mover.gameObject.SetActive(false);
    }

    private void OnDestroyObstacle(Obstacle mover)
    {
        Destroy(mover.gameObject);
    }
}
