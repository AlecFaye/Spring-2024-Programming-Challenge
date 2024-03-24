using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerMovement PlayerMovement;
    public PlayerStats PlayerStats;
    public PlayerAttack PlayerAttack;

    [SerializeField] private GameObject deathPanelUI;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogWarning("Found more than one Player in the scene.");

        Instance = this;

        deathPanelUI.SetActive(false);
    }

    private void Start()
    {
        PlayerStats.HealthSystem.OnDie += Player_OnDie;
    }

    private void Player_OnDie()
    {
        deathPanelUI.SetActive(true);
    }
}
