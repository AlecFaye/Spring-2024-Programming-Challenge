using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerMovement PlayerMovement;
    public PlayerStats PlayerStats;
    public PlayerAttack PlayerAttack;

    [SerializeField] private GameObject deathPanelUI;
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogWarning("Found more than one Player in the scene.");

        Instance = this;

        deathPanelUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    private void Start()
    {
        PlayerStats.HealthSystem.OnDie += Player_OnDie;
    }

    private void Player_OnDie()
    {
        deathPanelUI.SetActive(true);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Time.timeScale == 1.0f)
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
