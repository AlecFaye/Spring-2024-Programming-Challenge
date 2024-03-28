using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private const float DISABLE_TIMER = 3.0f;

    public EnemyStats EnemyStats;
    public EnemyMovement EnemyMovement;
    public EnemyAnimationController EnemyAnimationController;
    public EnemyAI EnemyAI;

    private ObjectPool<Enemy> pool;

    private void Start()
    {
        EnemyStats.HealthSystem.OnDie += Enemy_OnDie;
    }

    private void Enemy_OnDie()
    {
        StartCoroutine(DisableEnemy());
    }

    private void OnEnable()
    {
        if (EnemyStats.IsDead)
            EnemyStats.HealthSystem?.Revive();
    }

    public void SetPool(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }

    private IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(DISABLE_TIMER);

        pool.Release(this);
    }
}
