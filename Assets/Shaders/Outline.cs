using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] private float xThickness;
    [SerializeField] private float yThickness;
    [SerializeField] private Color outlineColour;

    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        InitMaterialsArray();
    }

    private void Start()
    {
        RemoveOutline();
    }

    public void DisplayOutline()
    {
        SetOutlineThickness(xThickness, yThickness, outlineColour);
    }

    public void RemoveOutline()
    {
        SetOutlineThickness(0.0f, 0.0f, Color.clear);
    }

    private void InitMaterialsArray()
    {
        materials = new Material[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
            materials[i] = spriteRenderers[i].material;
    }

    private void SetOutlineThickness(float xThickness, float yThickness, Color outlineColour)
    {
        foreach (Material material in materials)
            material.SetFloat("_Thickness_X", xThickness);

        foreach (Material material in materials)
            material.SetFloat("_Thickness_Y", yThickness);

        foreach (Material material in materials)
            material.SetColor("_OutlineColor", outlineColour);
    }
}
