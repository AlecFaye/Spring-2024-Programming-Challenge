using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private Outline shieldOutline;

    [Header("Health Configurations")]
    [SerializeField] private int healthAmount;
    [SerializeField] private int absoluteMinHealth;
    [SerializeField] private int absoluteMaxHealth;
    [SerializeField] private float invincibleFrames;

    private HealthSystem healthSystem;
    public HealthSystem HealthSystem => healthSystem;

    public bool IsDead => isDead;

    private bool isDead = false;
    private bool isShielded = false;

    private float time = 0.0f;
    private float timeWasHit = -999f;

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
        if (isDead || IsInvincible || isShielded)
            return;

        damageFlash.StartFlash();

        timeWasHit = time;

        healthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        if (isDead)
            return;

        healthSystem.Heal(healAmount);
    }

    public void Shield(float shieldDuration)
    {
        StartCoroutine(ShieldPlayer(shieldDuration));
    }

    public Transform GetTransform() => transform;

    private IEnumerator ShieldPlayer(float shieldDuration)
    {
        isShielded = true;
        shieldOutline.DisplayOutline();

        yield return new WaitForSeconds(shieldDuration);

        shieldOutline.RemoveOutline();
        isShielded = false;
    }
}
