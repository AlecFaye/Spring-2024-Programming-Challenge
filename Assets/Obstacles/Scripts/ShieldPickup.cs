using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] private float shieldDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable) && collision.CompareTag("Player"))
        {
            damageable.Shield(shieldDuration);
            gameObject.SetActive(false);
        }
    }
}
