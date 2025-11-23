using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHit : MonoBehaviour
{
    public int damage;
    public float delayDamageReader = 0;
    public float endDamageReader = 0;
    bool hasDamaged = false;
    bool canDamage = true;

    void OnEnable()
    {
        if (delayDamageReader == 0 && endDamageReader == 0) return;

        canDamage = false;
        Invoke(nameof(EnableDamageReader), delayDamageReader);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasDamaged && canDamage) 
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            if (healthManager.enabled) 
                healthManager.AdjustHealth(damage);
            hasDamaged = true;
        }
    }

    void EnableDamageReader()
    {
        canDamage = true;
        Invoke(nameof(DisableDamageReader), endDamageReader);
    }

    void DisableDamageReader()
    {
        canDamage = false;
    }
}
