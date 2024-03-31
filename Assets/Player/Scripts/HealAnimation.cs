using UnityEngine;

public class HealAnimation : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void FinishHeal()
    {
        gameObject.SetActive(false);
    }
}
