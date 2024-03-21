public class HealthSystem
{
    public delegate void HealthEvent();
    public HealthEvent OnDamaged;
    public HealthEvent OnHealed;
    public HealthEvent OnDie;
    public HealthEvent OnRevive;
    public HealthEvent OnMaxHealthChanged;

    private int maxHealthAmount;
    private int healthAmount;
    private readonly int absoluteMinAmount;
    private readonly int absoluteMaxAmount;

    public int HealthAmount => healthAmount;
    public int MaxHealthAmount => maxHealthAmount;
    public int AbsoluteMaxAmount => absoluteMaxAmount;

    public HealthSystem(int amount, int absoluteMinAmount, int absoluteMaxAmount)
    {
        healthAmount = amount;
        maxHealthAmount = amount;

        this.absoluteMinAmount = absoluteMinAmount;
        this.absoluteMaxAmount = absoluteMaxAmount;
    }

    public void Damage(int amount)
    {
        healthAmount -= amount;

        OnDamaged?.Invoke();

        if (healthAmount <= 0)
        {
            healthAmount = 0;
            OnDie?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        healthAmount = healthAmount + amount >= maxHealthAmount
            ? maxHealthAmount
            : healthAmount + amount;

        OnHealed?.Invoke();
    }

    public void Revive()
    {
        healthAmount = maxHealthAmount;

        OnRevive?.Invoke();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealthAmount += amount;

        if (maxHealthAmount > absoluteMaxAmount)
            maxHealthAmount = absoluteMaxAmount;

        OnMaxHealthChanged?.Invoke();
    }

    public void DecreaseMaxHealth(int amount)
    {
        maxHealthAmount -= amount;

        if (maxHealthAmount < absoluteMinAmount)
            maxHealthAmount = absoluteMinAmount;

        if (healthAmount > maxHealthAmount)
            healthAmount = maxHealthAmount;

        OnMaxHealthChanged?.Invoke();
    }

    public float GetHealthNormalized() => (float)healthAmount / maxHealthAmount;
}
