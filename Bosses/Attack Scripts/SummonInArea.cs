using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonInArea : MonoBehaviour
{
    [Header("Object References")]
    public GameObject prefab;
    public Transform center;
    public bool playerIsCenter = false; 

    [Header("Spawn Settings")]
    public int projectileCount = 1;
    public Vector2 areaSize = new Vector2(5f, 5f);
    public float fixedYPosition = 0f;

    [Header("Timing Settings")]
    public float initialWaitTime = 0f;
    public float cooldown = 0f;
    public float finalWaitTime = 0f;

    [Header("Animation Settings")]
    public string animTriggerName = "";

    [Header("Behavior Options")]
    public bool quickReset = false;

    BossAI bossAI;
    Animator animator;
    GameObject boss;

    void OnEnable()
    {
        boss = transform.root.gameObject;

        bossAI = boss.GetComponent<BossAI>();
        animator = boss.GetComponent<Animator>();

        if (quickReset)
            bossAI.InvokeReset();

        Invoke(nameof(StartCoroutineCall), initialWaitTime);
    }

    void StartCoroutineCall()
    {
        if (animTriggerName != "")
            animator.SetTrigger(animTriggerName);

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(prefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }

        yield return new WaitForSeconds(finalWaitTime);

        if (!quickReset)
            bossAI.InvokeReset();

        this.gameObject.SetActive(false);
    }

    Vector3 GetRandomPosition()
    {
        Transform centerTransform;

        if (playerIsCenter && bossAI != null && bossAI.player != null)
        {
            centerTransform = bossAI.player.transform;
        }
        else
        {
            centerTransform = center != null ? center : transform;
        }

        Vector3 centerPos = centerTransform.position;

        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);

        return new Vector3(centerPos.x + randomX, fixedYPosition, centerPos.z + randomZ);
    }
}
