using UnityEngine;

public class DamageHurtbox : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(null, damage);
    }
}
