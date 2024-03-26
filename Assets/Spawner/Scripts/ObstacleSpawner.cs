using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool<Mover> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    [HideInInspector] public Mover ObstaclePrefab;
    
    private GameObject parent;

    private void Start()
    {
        parent = new($"{ObstaclePrefab.name} (Object Pool)");
        Pool = new(CreateObstacle, OnGetMoverFromPool, OnReleaseMoverToPool, OnDestroyMover, true, defaultCapacity, maxSize);
    }

    private Mover CreateObstacle()
    {
        Mover obstacleObject = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity, parent.transform);

        obstacleObject.SetPool(Pool);

        return obstacleObject;
    }

    private void OnGetMoverFromPool(Mover mover)
    {
        mover.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        mover.gameObject.SetActive(true);
    }
    
    private void OnReleaseMoverToPool(Mover mover)
    {
        mover.gameObject.SetActive(false);
    }

    private void OnDestroyMover(Mover mover)
    {
        Destroy(mover.gameObject);
    }
}
