using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(IDamageable damager, int damageAmount);

    public abstract void Heal(int healAmount);

    public abstract void Shield(float shieldDuration);

    public abstract Transform GetTransform();
}
