using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool<Obstacle> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    [HideInInspector] public Obstacle ObstaclePrefab;
    
    private GameObject parent;

    private void Start()
    {
        parent = new($"{ObstaclePrefab.name} (Object Pool)");
        Pool = new(CreateObstacle, OnGetMoverFromPool, OnReleaseMoverToPool, OnDestroyMover, true, defaultCapacity, maxSize);
    }

    private Obstacle CreateObstacle()
    {
        Obstacle obstacleObject = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity, parent.transform);

        obstacleObject.SetPool(Pool);

        return obstacleObject;
    }

    private void OnGetMoverFromPool(Obstacle mover)
    {
        mover.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        mover.gameObject.SetActive(true);
    }
    
    private void OnReleaseMoverToPool(Obstacle mover)
    {
        mover.gameObject.SetActive(false);
    }

    private void OnDestroyMover(Obstacle mover)
    {
        Destroy(mover.gameObject);
    }
}
