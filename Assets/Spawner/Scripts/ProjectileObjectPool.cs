using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public ObjectPool<Projectile> Pool;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10000;

    public Projectile ProjectilePrefab;

    private GameObject parent;

    private void Start()
    {
        parent = new($"{ProjectilePrefab.name} (Object Pool)");
        Pool = new(CreateEnemy, OnGetProjectileFromPool, OnReleaseProjectileFromPool, OnDestroyProjectile, true, defaultCapacity, maxSize);
    }

    private Projectile CreateEnemy()
    {
        Projectile projectileObject = Instantiate(ProjectilePrefab, Vector3.zero, Quaternion.identity, parent.transform);

        projectileObject.SetPool(Pool);

        return projectileObject;
    }

    private void OnGetProjectileFromPool(Projectile projectile)
    {
        projectile.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectileFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
