using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;
    [SerializeField] private AnimationCurve flashAmountCurve;

    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private Coroutine flashCoroutine = null;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        InitMaterialsArray();
    }

    public void StartFlash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(Flash());
    }

    private void InitMaterialsArray()
    {
        materials = new Material[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
            materials[i] = spriteRenderers[i].material;
    }

    private IEnumerator Flash()
    {
        SetFlashColor();

        for (float time = 0.0f; time < flashTime; time += Time.deltaTime)
        {
            float currentFlashAmount = flashAmountCurve.Evaluate(time / flashTime);

            SetFlashAmount(currentFlashAmount);

            yield return null;
        }

        SetFlashAmount(0.0f);
    }

    private void SetFlashColor()
    {
        foreach (Material material in materials)
            material.SetColor("_FlashColor", flashColor);
    }

    private void SetFlashAmount(float flashAmount)
    {
        foreach (Material material in materials)
            material.SetFloat("_FlashAmount", flashAmount);
    }
}
