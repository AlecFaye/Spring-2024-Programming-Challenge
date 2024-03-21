using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack") || collision.CompareTag("Enemy Attack"))
            collision.gameObject.SetActive(false);
    }
}
