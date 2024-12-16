using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHit : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            if (healthManager.enabled) healthManager.AdjustHealth(damage);
        }
    }
}
