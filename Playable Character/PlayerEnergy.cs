using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Energy Settings")]
    public float maxEnergy = 100f;
    public float refillRate = 10f;
    public float refillDelay = 2f;

    [Header("UI")]
    public Image energyBar;

    private float currentEnergy;
    private float lastEnergyUseTime;
    private bool isRefilling = false;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    void Update()
    {
        if (!isRefilling && Time.time - lastEnergyUseTime >= refillDelay && currentEnergy < maxEnergy)
        {
            StartCoroutine(AutoRefill());
        }

        UpdateEnergyBar();
    }

    private IEnumerator AutoRefill()
    {
        isRefilling = true;

        while (currentEnergy < maxEnergy)
        {
            currentEnergy += refillRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
            UpdateEnergyBar();
            yield return null;
        }

        isRefilling = false;
    }

    public bool LowerEnergy(float energyCost)
    {
        if (currentEnergy - energyCost < 0f)
            return false;

        currentEnergy -= energyCost;
        lastEnergyUseTime = Time.time;
        UpdateEnergyBar();
        return true;
    }

    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
            energyBar.fillAmount = GetEnergyPercent();
    }
}
