using System.Collections;
using UnityEngine;

public class BalteusRocket : MonoBehaviour
{
    [Header("References")]
    public BossAI bossAI;

    [Header("Rocket Settings")]
    public float speed = 40f;

    [Tooltip("How long the rocket flies straight before homing")]
    public float initialDelay = 0.5f;

    [Tooltip("Maximum time it is allowed to home")]
    public float homingDuration = 3f;

    [Tooltip("Degrees per second while homing")]
    public float turnSpeed = 90f;

    [Header("Rotation Offset")]
    [Tooltip("Offset applied to the rocket's rotation to define where the front actually is")]
    public Vector3 rotationOffsetEuler;

    [Header("Target Adjustment")]
    [Tooltip("Offset applied to target height so rockets aim higher")]
    public float targetYOffset = 0f;

    [Header("Lifetime")]
    public float destroyAfter = 12f;

    private bool homingStopped = false;
    private Transform target;
    private Quaternion rotationOffset;

    void Start()
    {
        if (bossAI == null)
            bossAI = FindAnyObjectByType<BossAI>();

        target = bossAI?.player.transform;

        rotationOffset = Quaternion.Euler(rotationOffsetEuler);

        StartCoroutine(HomingRoutine());
        Destroy(gameObject, destroyAfter);
    }

    IEnumerator HomingRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        float startTime = Time.time;

        while (!homingStopped && Time.time - startTime < homingDuration)
        {
            if (target != null)
            {
                Vector3 targetPos = target.position + Vector3.up * targetYOffset;

                Vector3 dir = (targetPos - transform.position).normalized;

                Vector3 forwardWithOffset = rotationOffset * Vector3.forward;

                float angle = Vector3.Angle(transform.rotation * forwardWithOffset, dir);

                if (angle < 1.5f)
                {
                    homingStopped = true;
                    break;
                }

                Quaternion targetRot = Quaternion.LookRotation(dir) * Quaternion.Inverse(rotationOffset);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    turnSpeed * Time.deltaTime
                );
            }

            yield return null;
        }
    }

    void Update()
    {
        Vector3 forward = transform.rotation * (rotationOffset * Vector3.forward);
        transform.position += forward * speed * Time.deltaTime;
    }
}
