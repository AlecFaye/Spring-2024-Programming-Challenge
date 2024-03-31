using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IBank bank) && collision.CompareTag("Player"))
        {
            bank.Deposit(coinAmount);
            gameObject.SetActive(false);
        }
    }
}
