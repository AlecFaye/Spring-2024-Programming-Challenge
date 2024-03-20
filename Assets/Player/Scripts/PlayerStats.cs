using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;

    [Header("Health Configurations")]
    [SerializeField] private int healthAmount;
    [SerializeField] private int absoluteMinHealth;
    [SerializeField] private int absoluteMaxHealth;

    [Header("Other Configurations")]
    [SerializeField] private float invincibleFrames;

    private HealthSystem healthSystem;
    public HealthSystem HealthSystem => healthSystem;

    public bool IsDead => isDead;

    private bool isDead = false;

    private float time = 0.0f;
    private float timeWasHit = 0.0f;

    public bool IsInvincible => timeWasHit + invincibleFrames > time;

    private void Awake()
    {
        healthSystem = new(healthAmount, absoluteMinHealth, absoluteMaxHealth);

        healthBar.Init(healthSystem);
    }

    private void Start()
    {
        healthSystem.OnDie += Player_OnDie;
        healthSystem.OnRevive += Player_OnRevive;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void Player_OnDie()
    {
        isDead = true;
    }

    private void Player_OnRevive()
    {
        isDead = false;
    }

    public void TakeDamage(IDamageable damager, int damageAmount)
    {
        if (isDead || IsInvincible)
            return;

        timeWasHit = time;

        healthSystem.Damage(damageAmount);
    }

    public void Heal(IDamageable healer, int healAmount)
    {
        if (isDead)
            return;

        healthSystem.Heal(healAmount);
    }

    public Transform GetTransform() => transform;
}
