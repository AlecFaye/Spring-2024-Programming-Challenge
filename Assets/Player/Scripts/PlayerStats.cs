using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IBank
{
    [Header("References")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private Outline shieldOutline;
    [SerializeField] private GameObject healAnimation;

    [Header("Health Configurations")]
    [SerializeField] private int healthAmount;
    [SerializeField] private int absoluteMinHealth;
    [SerializeField] private int absoluteMaxHealth;
    [SerializeField] private float invincibleFrames;

    [Header("Sound Configurations")]
    [SerializeField] private AudioClip healAudioClip;
    [SerializeField] private AudioClip shieldAudioClip;
    [SerializeField] private AudioClip coinAudioClip;
    [SerializeField] private AudioClip hurtAudioClip;

    [Header("Coins Configurations")]
    [SerializeField] private TextMeshProUGUI coinsText;

    private HealthSystem healthSystem;
    public HealthSystem HealthSystem => healthSystem;

    public bool IsDead => isDead;

    private bool isDead = false;
    private bool isShielded = false;

    private float time = 0.0f;
    private float timeWasHit = -999f;

    private Coroutine shieldCoroutine = null;

    private int coins = 0;

    public bool IsInvincible => timeWasHit + invincibleFrames > time;

    private void Awake()
    {
        healthSystem = new(healthAmount, absoluteMinHealth, absoluteMaxHealth);

        healthBar.Init(healthSystem);

        coinsText.text = $"{coins}";
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
        Time.timeScale = 1.0f;
        isDead = false;
    }

    public void TakeDamage(IDamageable damager, int damageAmount)
    {
        if (isDead || IsInvincible || isShielded)
            return;

        timeWasHit = time;
        damageFlash.StartFlash();
        healthSystem.Damage(damageAmount);

        AudioManager.Instance.PlaySFX(hurtAudioClip);
    }

    public void Heal(int healAmount)
    {
        if (isDead)
            return;

        healthSystem.Heal(healAmount);
        healAnimation.SetActive(true);

        AudioManager.Instance.PlaySFX(healAudioClip);
    }

    public void Shield(float shieldDuration)
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldPlayer(shieldDuration));
    }

    public Transform GetTransform() => transform;

    private IEnumerator ShieldPlayer(float shieldDuration)
    {
        isShielded = true;
        shieldOutline.DisplayOutline();

        AudioManager.Instance.PlaySFX(shieldAudioClip);

        yield return new WaitForSeconds(shieldDuration);

        shieldOutline.RemoveOutline();
        isShielded = false;
    }

    public void Deposit(int amount)
    {
        if (IsDead)
            return;

        coins += amount;
        coinsText.text = $"{coins}";

        AudioManager.Instance.PlaySFX(coinAudioClip);
    }

    public int GetBalance() => coins;
}
