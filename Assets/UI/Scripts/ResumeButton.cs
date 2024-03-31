using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1.0f;
        });
    }
}
