using UnityEngine;
using UnityEngine.UI;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Sprite fullHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;

    public void GainHealth()
    {
        healthImage.sprite = fullHealthSprite;
    }

    public void LoseHealth()
    {
        healthImage.sprite = emptyHealthSprite;
    }
}
