using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 100;
    public bool playerProjectile = false;
    void OnCollisionEnter(Collision collision)
    {
        if (!playerProjectile && !collision.gameObject.CompareTag("Player")) return;

        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (healthManager != null) healthManager.AdjustHealth(damage);
    }
}
