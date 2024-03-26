using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public EnemyStats EnemyStats;
    public EnemyMovement EnemyMovement;
    public EnemyAnimationController EnemyAnimationController;

    private ObjectPool<Enemy> pool;

    private void Awake()
    {
        EnemyAnimationController.OnFinishDeath += Enemy_OnFinishDeath;
    }

    private void OnDisable()
    {
        EnemyStats.HealthSystem.OnDie -= Spawner.Instance.Spawn_BossDefeated;
    }

    private void Enemy_OnFinishDeath()
    {
        pool.Release(this);
    }

    public void SetPool(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }
}
