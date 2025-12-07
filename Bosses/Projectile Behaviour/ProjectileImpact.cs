using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    [Header("Impact Settings")]
    [Tooltip("Effect spawned when the projectile hits something.")]
    public GameObject impactEffect;

    [Tooltip("How long before the projectile object is destroyed after impact.")]
    public float destroyDelay = 0f;

    [Tooltip("Should the impact effect be destroyed automatically?")]
    public bool autoDestroyEffect = true;

    [Tooltip("How long the spawned effect should exist (optional).")]
    public float effectLifetime = 2f;

    private bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) return;

        HandleHit(transform.position, transform.rotation);
    }

    private void HandleHit(Vector3 hitPosition, Quaternion hitRotation)
    {
        if (hasHit) return;
        hasHit = true;

        // Spawn impact effect
        if (impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect, hitPosition, hitRotation);

            if (autoDestroyEffect)
            {
                Destroy(effect, effectLifetime);
            }
        }

        // Destroy projectile
        Destroy(gameObject, destroyDelay);
    }
}
