using System.Collections.Generic;
using UnityEngine;

public class ConstantHitCheck : MonoBehaviour
{
    public int damage = 1;
    public float damageInterval = 1f;
    public float disableTimer = 0f;
    public float delayStartReader = 0f;

    bool isDisabled = false;

    private readonly Dictionary<HealthManager, float> nextDamageTime = new();

    void Start()
    {
        if (delayStartReader > 0)
        {
            isDisabled = true;
            Invoke(nameof(EnableReader), delayStartReader);
        }
        if (disableTimer > 0) Invoke(nameof(DisableReader), disableTimer);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || isDisabled) return;

        HealthManager health = other.GetComponent<HealthManager>();
        if (health == null || !health.enabled) return;

        float now = Time.time;

        if (!nextDamageTime.TryGetValue(health, out float nextTime))
            nextTime = 0f;

        if (now >= nextTime)
        {
            health.AdjustHealth(damage);
            nextDamageTime[health] = now + damageInterval;
        }
    }

    void DisableReader()
    {
        isDisabled = true;
    }

    void EnableReader()
    {
        isDisabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent<HealthManager>(out var health))
        {
            nextDamageTime.Remove(health);
        }
    }
}
