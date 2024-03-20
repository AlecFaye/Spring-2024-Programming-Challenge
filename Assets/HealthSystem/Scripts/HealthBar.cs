using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthContainer healthContainerPrefab;
    [SerializeField] private Transform healthContainerTF;

    private readonly List<HealthContainer> healthContainers = new();
    private HealthSystem healthSystem;

    public void Init(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        CreateHealthContainers();
        ToggleHealthContainers();

        SetUpHealthEvents();
    }

    private void HealthSystem_OnDamaged()
    {
        UpdateHealthContainers();
    }

    private void HealthSystem_OnHealed()
    {
        UpdateHealthContainers();
    }

    private void HealthSystem_OnRevive()
    {
        UpdateHealthContainers();
    }

    private void HealthSystem_OnMaxHealthChanged()
    {
        foreach (HealthContainer healthContainer in healthContainers)
            healthContainer.gameObject.SetActive(false);

        ToggleHealthContainers();
        UpdateHealthContainers();
    }

    private void SetUpHealthEvents()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnRevive += HealthSystem_OnRevive;
        healthSystem.OnMaxHealthChanged += HealthSystem_OnMaxHealthChanged;
    }

    private void CreateHealthContainers()
    {
        for (int i = 0; i < healthSystem.AbsoluteMaxAmount; i++)
        {
            HealthContainer healthContainer = Instantiate(healthContainerPrefab, healthContainerTF);
            healthContainers.Add(healthContainer);
            healthContainer.gameObject.SetActive(false);
        }
    }

    private void ToggleHealthContainers()
    {
        for (int index = 0; index < healthSystem.MaxHealthAmount; index++)
            healthContainers[index].gameObject.SetActive(true);
    }

    private void UpdateHealthContainers()
    {
        for (int index = 0; index < healthSystem.MaxHealthAmount; index++)
        {
            if (index < healthSystem.HealthAmount)
            {
                healthContainers[index].GainHealth();
            }
            else
            {
                healthContainers[index].LoseHealth();
            }
        }
    }
}
