using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private DamageFlash damageFlash;

    [Header("Health Configurations")]
    [SerializeField] private int baseHealth;
    [SerializeField] private int absoluteMinHealth;
    [SerializeField] private int absoluteMaxHealth;

    public HealthSystem HealthSystem { get; private set; }

    public bool IsDead { get; private set; }

    private void Awake()
    {
        HealthSystem = new(baseHealth, absoluteMinHealth, absoluteMaxHealth);
    }

    private void Start()
    {
        HealthSystem.OnDie += Enemy_OnDie;
        HealthSystem.OnRevive += Enemy_OnRevive;
        HealthSystem.OnDie += Spawner.Instance.Spawn_BossDefeated;
    }

    private void Enemy_OnDie()
    {
        IsDead = true;
    }

    private void Enemy_OnRevive()
    {
        IsDead = false;
    }

    public void TakeDamage(IDamageable damager, int damageAmount)
    {
        if (IsDead)
            return;

        damageFlash.StartFlash();

        HealthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        if (IsDead)
            return;

        HealthSystem.Heal(healAmount);
    }

    public void Shield(float shieldDuration) { }

    public Transform GetTransform() => transform;
}
