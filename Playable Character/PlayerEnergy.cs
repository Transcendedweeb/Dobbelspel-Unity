using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float refillRate = 10f;
    public float refillDelay = 2f;

    float currentEnergy;
    float lastEnergyUseTime;
    bool isRefilling = false;

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (!isRefilling && Time.time - lastEnergyUseTime >= refillDelay && currentEnergy < maxEnergy)
        {
            StartCoroutine(AutoRefill());
        }
    }

    IEnumerator AutoRefill()
    {
        isRefilling = true;

        while (currentEnergy < maxEnergy)
        {
            currentEnergy += refillRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
            yield return null;
        }

        isRefilling = false;
    }

    public bool LowerEnergy(float energyCost)
    {
        if (currentEnergy - energyCost < 0) return false;
        else
        {
            currentEnergy -= energyCost;
            lastEnergyUseTime = Time.time;
            return true;
        }
    }

    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }
}
