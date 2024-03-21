using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private DamageFlash damageFlash;

    [Header("Health Configurations")]
    [SerializeField] private int baseHealth;
    [SerializeField] private int absoluteMinHealth;
    [SerializeField] private int absoluteMaxHealth;

    private HealthSystem healthSystem;
    public HealthSystem HealthSystem => healthSystem;

    private bool isDead = false;

    private void Awake()
    {
        healthSystem = new(baseHealth, absoluteMinHealth, absoluteMaxHealth);
    }

    private void Start()
    {
        healthSystem.OnDie += Enemy_OnDie;
    }

    private void Enemy_OnDie()
    {
        isDead = true;
    }

    public void TakeDamage(IDamageable damager, int damageAmount)
    {
        if (isDead)
            return;

        damageFlash.StartFlash();

        healthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        if (isDead)
            return;

        healthSystem.Heal(healAmount);
    }

    public void Shield(float shieldDuration) { }

    public Transform GetTransform() => transform;
}
