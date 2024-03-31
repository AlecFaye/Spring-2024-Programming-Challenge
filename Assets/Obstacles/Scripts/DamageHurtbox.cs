using UnityEngine;

public class DamageHurtbox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask damageLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable) && IsLayerInLayerMask(collision.gameObject.layer, damageLayer))
            damageable.TakeDamage(null, damage);
    }

    private bool IsLayerInLayerMask(int layer, LayerMask layerMask) => layerMask == (layerMask | (1 << layer));
}
