using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    public int damage = 100;
    public bool playerProjectile = false;
    public HS_ProjectileMover hS_ProjectileMover;

    void OnTriggerEnter(Collider other)
    {
        if (!playerProjectile && !other.CompareTag("Player")) return;

        if (hS_ProjectileMover != null)
            hS_ProjectileMover.HandleTrigger(other);

        if (other.TryGetComponent<HealthManager>(out var healthManager))
            healthManager.AdjustHealth(damage);
    }
}
