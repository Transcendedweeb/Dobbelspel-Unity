using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindupDashAttack : MonoBehaviour
{
    [Header("Object References")]
    public GameObject mainParent;
    public GameObject prefab;
    public GameObject player;

    [Header("Windup (Start)")]
    public string windupAnimTrigger = "Windup";
    public float windupTime = 1.0f;

    [Header("Dash (Attack)")]
    public string attackAnimTrigger = "Attack";
    public float dashSpeed = 6f;
    public float attackDistance = 1.0f;
    public Vector3 dashPositionOffset = Vector3.zero;
    public Vector3 spawnOffset = Vector3.zero;

    [Header("Position / Rotation Options")]
    public Vector3 rotationOffset = Vector3.zero;
    public bool useFixedY = false;
    public float fixedYPosition = 0f;

    [Header("Behaviour")]
    public bool quickReset = false;
    public float animWaitTimeAfterArrival = 0.1f;
    public float endWaitTime = 0f;

    private Animator animator;
    private BossAI bossAI;
    private LockOn lockOn;
    private Vector3 recordedTargetPosition;
    private bool isDashing = false;

    void OnEnable()
    {
        if (mainParent != null)
        {
            animator = mainParent.GetComponent<Animator>();
            bossAI = mainParent.GetComponent<BossAI>();
            lockOn = mainParent.GetComponentInChildren<LockOn>();
        }
        else
        {
            Debug.LogWarning("WindupDashAttack: mainParent is not assigned.");
        }

        if (quickReset && bossAI != null)
        {
            bossAI.InvokeReset();
        }

        StartCoroutine(WindupThenDash());
    }

    IEnumerator WindupThenDash()
    {
        if (animator != null && !string.IsNullOrEmpty(windupAnimTrigger))
            animator.SetTrigger(windupAnimTrigger);

        float t = 0f;
        while (t < windupTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (player != null)
            recordedTargetPosition = player.transform.position;
        else
            recordedTargetPosition = transform.position;

        if (useFixedY)
            recordedTargetPosition.y = fixedYPosition;

        recordedTargetPosition += dashPositionOffset;

        if (animator != null && !string.IsNullOrEmpty(attackAnimTrigger))
            animator.SetTrigger(attackAnimTrigger);

        SetLockOn(false);

        isDashing = true;
        yield return StartCoroutine(DashToRecordedPosition());
        isDashing = false;

        SetLockOn(true);

        if (animWaitTimeAfterArrival > 0f)
            yield return new WaitForSeconds(animWaitTimeAfterArrival);

        DoAttack();

        if (endWaitTime > 0f)
            yield return new WaitForSeconds(endWaitTime);

        End();
    }

    IEnumerator DashToRecordedPosition()
    {
        float effectiveSpeed = Mathf.Max(0.001f, dashSpeed);

        while (true)
        {
            Vector3 currentPos = mainParent != null ? mainParent.transform.position : transform.position;
            Vector3 toTarget = recordedTargetPosition - currentPos;
            float dist = toTarget.magnitude;

            if (dist <= attackDistance)
                break;

            Vector3 dir = toTarget.normalized;
            if (mainParent != null)
                mainParent.transform.position += dir * effectiveSpeed * Time.deltaTime;
            else
                transform.position += dir * effectiveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    void DoAttack()
    {
        if (prefab == null) return;

        Vector3 spawnPos = (mainParent != null ? mainParent.transform.position : transform.position) + spawnOffset;

        Quaternion lookRotation = Quaternion.identity;
        Vector3 dir = (recordedTargetPosition - spawnPos);
        if (dir.sqrMagnitude > 0.001f)
            lookRotation = Quaternion.LookRotation(dir.normalized);

        lookRotation *= Quaternion.Euler(rotationOffset);

        Instantiate(prefab, spawnPos, lookRotation);
    }

    void SetLockOn(bool set)
    {
        if (lockOn == null)
        {
            lockOn = mainParent != null
                ? mainParent.GetComponentInChildren<LockOn>()
                : GetComponentInChildren<LockOn>();
        }

        if (lockOn != null)
        {
            lockOn.enabled = set;
            Debug.Log($"[WindupDashAttack] LockOn {(set ? "enabled" : "disabled")}.");
        }
        else
        {
            Debug.LogWarning("[WindupDashAttack] No LockOn component found!");
        }
    }

    void End()
    {
        if (!quickReset && bossAI != null)
            bossAI.InvokeReset();

        isDashing = false;
        this.gameObject.SetActive(false);
    }

    public void CancelAndEnd()
    {
        StopAllCoroutines();
        End();
    }
}
