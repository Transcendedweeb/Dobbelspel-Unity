using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePlayer : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefab;
    public Vector3 prefabPositionOffset;
    public Vector3 prefabRotationOffset;

    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float minDistance = .5f;
    public float durationInSeconds = 5f;

    [Header("Animation Settings")]
    public string animBoolName = "";
    public float finalWaitTime = .1f;

    [Header("Behavior Options")]
    public bool quickReset = false;

    Animator animator;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;
    GameObject instantiatedPrefab;
    GameObject mainParent;

    void OnEnable()
    {
        mainParent = transform.root.gameObject;

        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();
        playerRefProvider = mainParent.GetComponent<PlayerReferenceProvider>();

        if (animBoolName != "")
            animator.SetBool(animBoolName, true);

        if (prefab != null)
            InvokePrefab();

        if (quickReset)
            bossAI.InvokeReset();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationInSeconds)
        {
            Vector3 playerPos = playerRefProvider.GetPlayerPosition();
            float distanceToPlayer = Vector3.Distance(transform.position, playerPos);

            if (distanceToPlayer > minDistance)
            {
                Vector3 direction = (playerPos - transform.position).normalized;
                mainParent.transform.position += movementSpeed * Time.deltaTime * direction;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (animBoolName != "")
            animator.SetBool(animBoolName, false);

        Invoke("End", finalWaitTime);
    }

    void InvokePrefab()
    {
        Vector3 spawnPosition = gameObject.transform.position + prefabPositionOffset;
        Vector3 playerPos = playerRefProvider.GetPlayerPosition();
        Vector3 directionToPlayer = (playerPos - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        Quaternion spawnRotation = lookRotation * Quaternion.Euler(prefabRotationOffset);

        instantiatedPrefab = Instantiate(prefab, spawnPosition, spawnRotation, mainParent.transform);
    }

    void End()
    {
        if (instantiatedPrefab != null)
            Destroy(instantiatedPrefab);

        if (!quickReset)
            bossAI.InvokeReset();

        this.gameObject.SetActive(false);
    }
}