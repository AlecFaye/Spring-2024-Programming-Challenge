using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(IDamageable damager, int damageAmount);

    public abstract void Heal(IDamageable healer, int healAmount);

    public abstract Transform GetTransform();
}
