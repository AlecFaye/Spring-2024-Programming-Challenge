using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool<Enemy> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    [HideInInspector] public Enemy EnemyPrefab;

    private GameObject parent;

    private void Start()
    {
        parent = new($"{EnemyPrefab.name} (Object Pool)");
        Pool = new(CreateEnemy, OnGetEnemyFromPool, OnReleaseEnemyFromPool, OnDestroyEnemy, true, defaultCapacity, maxSize);
    }

    private Enemy CreateEnemy()
    {
        Enemy enemyObject = Instantiate(EnemyPrefab, Vector3.zero, Quaternion.identity, parent.transform);

        enemyObject.SetPool(Pool);

        return enemyObject;
    }

    private void OnGetEnemyFromPool(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemyFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
