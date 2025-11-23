using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SummonOnPlayer : MonoBehaviour
{
    [Header("Object References")]
    public GameObject prefab;

    [Header("Spawn Settings")]
    public int projectileCount = 1;
    public float fixedYPosition = 0f;

    [Header("Timing Settings")]
    public float initialWaitTime = 0f;
    public float delayAttackAtAnimStart = 0f;
    public float cooldown = 0f;

    [Header("Animation Settings")]
    public string animTriggerName = "";

    [Header("Behavior Options")]
    public bool quickReset = false;

    // Private fields
    BossAI bossAI;
    Animator animator;
    GameObject boss;
    PlayerReferenceProvider playerRefProvider;

    void OnEnable()
    {
        boss = transform.root.gameObject;

        bossAI = boss.GetComponent<BossAI>();
        animator = boss.GetComponent<Animator>();
        playerRefProvider = boss.GetComponent<PlayerReferenceProvider>();

        if (quickReset)
            bossAI.InvokeReset();

        Invoke(nameof(StartCoroutineCall), initialWaitTime);
    }

    void StartCoroutineCall()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        if (animTriggerName != "")
            animator.SetTrigger(animTriggerName);

        if (delayAttackAtAnimStart > 0f)
            yield return new WaitForSeconds(delayAttackAtAnimStart);

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 playerPos = playerRefProvider.GetPlayerPosition();
            Vector3 spawnPosition = new(
                playerPos.x,
                fixedYPosition,
                playerPos.z
            );

            Instantiate(prefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }

        if (!quickReset)
            bossAI.InvokeReset();

        this.gameObject.SetActive(false);
    }
}
