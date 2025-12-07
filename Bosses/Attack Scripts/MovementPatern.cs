using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    [Header("Path Settings (Recommended)")]
    public bool useChildPoints = true;
    public Transform pointsParent;

    [Header("Manual Path (Optional)")]
    public List<Vector3> localPoints = new List<Vector3>();

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float pointArrivalDistance = 0.2f;
    public float waitAtPoint = 0.2f;
    public bool loopPattern = false;

    [Header("Rotation")]
    public float rotationSpeed = 7f;
    public Vector3 rotationOffset;

    [Header("Model Orientation")]
    public float modelForwardY = 0f;

    [Header("Prefab Spawn")]
    public GameObject spawnPrefab;
    public Vector3 prefabSpawnOffset;

    [Header("Animation")]
    public string startAnimTrigger = "";
    public float startAnimWait = 0.1f;
    public string endAnimTrigger = "";
    public float endAnimWait = 0.1f;

    [Header("Behavior")]
    public bool disableLockOn = true;
    public bool quickReset = false;

    [Header("LockOn Smooth Return")]
    public float lockOnBlendTime = 0.5f;
    public float lockOnRotationSpeed = 7f;

    private BossAI bossAI;
    private Animator animator;
    private Transform bossRoot;
    private LockOn lockOn;

    void OnEnable()
    {
        bossRoot = transform.root;
        bossAI = bossRoot.GetComponent<BossAI>();
        animator = bossRoot.GetComponent<Animator>();
        lockOn = bossRoot.GetComponent<LockOn>();

        if (quickReset)
            bossAI.InvokeReset();

        if (disableLockOn && lockOn != null)
            lockOn.enabled = false;

        if (startAnimTrigger != "")
            animator.SetTrigger(startAnimTrigger);

        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(startAnimWait);

        if (spawnPrefab != null)
        {
            Vector3 spawnPos = bossRoot.position + prefabSpawnOffset;
            Instantiate(spawnPrefab, spawnPos, spawnPrefab.transform.rotation);
        }

        StartCoroutine(RunPattern());
    }


    List<Vector3> BuildWorldPoints()
    {
        List<Vector3> worldPoints = new List<Vector3>();

        if (useChildPoints && pointsParent != null && pointsParent.childCount > 0)
        {
            localPoints.Clear();
            for (int i = 0; i < pointsParent.childCount; i++)
                localPoints.Add(pointsParent.GetChild(i).localPosition);
        }

        foreach (Vector3 p in localPoints)
            worldPoints.Add(bossRoot.TransformPoint(p));

        return worldPoints;
    }


    IEnumerator RunPattern()
    {
        List<Vector3> path = BuildWorldPoints();

        if (path.Count == 0)
        {
            Debug.LogWarning("MovementPatternAttack: No points found!");
            EndAttack();
            yield break;
        }

        int index = 0;

        while (true)
        {
            Vector3 target = path[index];
            Vector3 direction = target - bossRoot.position;

            if (direction != Vector3.zero)
            {
                Quaternion lookRot =
                    Quaternion.LookRotation(direction.normalized) *
                    Quaternion.Euler(rotationOffset + new Vector3(0f, modelForwardY, 0f));

                bossRoot.rotation = Quaternion.Lerp(
                    bossRoot.rotation,
                    lookRot,
                    rotationSpeed * Time.deltaTime
                );
            }

            bossRoot.position += direction.normalized * moveSpeed * Time.deltaTime;

            if (direction.magnitude <= pointArrivalDistance)
            {
                yield return new WaitForSeconds(waitAtPoint);

                index++;

                if (index >= path.Count)
                {
                    if (loopPattern)
                        index = 0;
                    else
                        break;
                }
            }

            yield return null;
        }

        EndAttack();
    }

    void EndAttack()
    {
        if (endAnimTrigger != "")
            animator.SetTrigger(endAnimTrigger);

        StartCoroutine(EndDelay());
    }

    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(endAnimWait);

        if (disableLockOn && lockOn != null)
        {
            yield return StartCoroutine(SmoothEnableLockOn());
        }

        if (!quickReset)
            bossAI.InvokeReset();

        gameObject.SetActive(false);
    }


    IEnumerator SmoothEnableLockOn()
    {
        Transform player = bossAI.player.transform;
        float timer = 0f;

        while (timer < lockOnBlendTime)
        {
            Vector3 dir = player.position - bossRoot.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot =
                    Quaternion.LookRotation(dir) *
                    Quaternion.Euler(0f, modelForwardY, 0f);

                bossRoot.rotation = Quaternion.Lerp(
                    bossRoot.rotation,
                    targetRot,
                    Time.deltaTime * lockOnRotationSpeed
                );
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Vector3 finalDir = player.position - bossRoot.position;
        finalDir.y = 0f;

        if (finalDir.sqrMagnitude > 0.001f)
            bossRoot.rotation = Quaternion.LookRotation(finalDir) *
                                Quaternion.Euler(0f, modelForwardY, 0f);

        lockOn.enabled = true;
    }
}
