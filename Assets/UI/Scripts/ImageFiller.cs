using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    [SerializeField] private Image imageToFill;
    [SerializeField] private float buffer = 1.0f;

    private float time = 0.0f;
    private float timeToFill = 0.0f;

    private bool isStartingToFill = false;

    private void Awake()
    {
        imageToFill.fillAmount = 0.0f;

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isStartingToFill = false;
    }

    private void Update()
    {
        if (!isStartingToFill)
            return;

        time += Time.deltaTime;

        imageToFill.fillAmount = Mathf.Clamp(time / timeToFill, 0.0f, 1.0f);

        if (time > timeToFill + buffer)
            gameObject.SetActive(false);
    }

    public void StartFill(float timeToFill)
    {
        time = 0.0f;
        this.timeToFill = timeToFill;
        isStartingToFill = true;
    }
}
