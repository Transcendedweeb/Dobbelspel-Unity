using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFromPoints : MonoBehaviour
{
    [Header("Prefab To Spawn")]
    public GameObject prefab;

    [Header("Spawn Points (Auto-Grab Children)")]
    public bool useChildPoints = true;
    public Transform[] customPoints;

    [Header("Spawn Behaviour")]
    public bool spawnAtAllPoints = true;
    public bool randomOrder = false;
    public bool inheritRotation = false;
    public Vector3 positionOffset;

    [Tooltip("If true, the spawned prefab becomes a child of the boss root.")]
    public bool spawnAsChildOfBoss = false;

    [Header("Timing")]
    public float initialDelay = 0f;
    public float betweenSpawns = 0.2f;
    public float endDelay = 0f;

    [Header("VFX / Animation / SFX")]
    public string animTriggerName = "";

    [Header("Reset Logic")]
    public bool quickReset = false;
    
    private BossAI bossAI;
    private Animator anim;
    private List<Transform> spawnPoints = new();
    private Transform bossRoot;

    void OnEnable()
    {
        bossRoot = transform.root;
        bossAI = bossRoot.GetComponent<BossAI>();
        anim = bossRoot.GetComponent<Animator>();

        if (quickReset)
            bossAI.InvokeReset();

        CachePoints();
        Invoke(nameof(StartAttack), initialDelay);
    }

    void CachePoints()
    {
        spawnPoints.Clear();

        if (useChildPoints)
        {
            foreach (Transform child in transform)
                spawnPoints.Add(child);
        }
        else
        {
            spawnPoints.AddRange(customPoints);
        }
    }

    void StartAttack()
    {
        if (!string.IsNullOrEmpty(animTriggerName))
            anim.SetTrigger(animTriggerName);

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("SummonFromPoints: No spawn points found.");
            EndAttack();
            yield break;
        }

        List<Transform> points = new(spawnPoints);

        if (randomOrder)
            Shuffle(points);

        if (spawnAtAllPoints)
        {
            foreach (Transform p in points)
            {
                Spawn(p);
                yield return new WaitForSeconds(betweenSpawns);
            }
        }
        else
        {
            Transform p = points[Random.Range(0, points.Count)];
            Spawn(p);
            yield return new WaitForSeconds(betweenSpawns);
        }

        yield return new WaitForSeconds(endDelay);
        EndAttack();
    }

    void Spawn(Transform point)
    {
        Vector3 spawnPos = point.position + positionOffset;
        Quaternion rot = inheritRotation
            ? point.rotation
            : prefab.transform.rotation;
            
        GameObject instance = Instantiate(prefab, spawnPos, rot);

        if (spawnAsChildOfBoss)
            instance.transform.SetParent(bossRoot, true);
    }

    void EndAttack()
    {
        if (!quickReset)
            bossAI.InvokeReset();

        gameObject.SetActive(false);
    }

    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}