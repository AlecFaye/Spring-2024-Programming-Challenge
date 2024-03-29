using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    [SerializeField] private ParryAttack parryAttack;
    [SerializeField] private Collider2D parryCollider;

    private void Awake()
    {
        parryCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack") && collision.TryGetComponent(out Projectile projectile))
        {
            projectile.gameObject.SetActive(false);
            parryAttack.Parry();
        }
    }

    public void EnableCollider()
    {
        parryCollider.enabled = true;
    }

    public void DisableCollider()
    {
        parryCollider.enabled = false;
    }
}
