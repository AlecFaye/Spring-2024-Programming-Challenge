using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (LevelLoader.Instance != null)
            {
                LevelLoader.Instance.LoadLevel(sceneName);
                Time.timeScale = 1.0f;
            }
        });
    }
}
