using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOnPlayer : MonoBehaviour
{
    [Header("Object References")]
    public GameObject boss;
    public GameObject prefab;
    public GameObject player;

    [Header("Spawn Settings")]
    public int projectileCount = 1;
    public float fixedYPosition = 0f;

    [Header("Timing Settings")]
    public float initialWaitTime = 0f;
    public float cooldown = 0f;

    [Header("Animation Settings")]
    public string animTriggerName = "";

    [Header("Behavior Options")]
    public bool quickReset = false;

    // Private fields
    BossAI bossAI;
    Animator animator;

    void OnEnable()
    {
        bossAI = boss.GetComponent<BossAI>();
        animator = boss.GetComponent<Animator>();

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

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                player.transform.position.x,
                fixedYPosition,
                player.transform.position.z
            );

            Instantiate(prefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }

        if (!quickReset)
            bossAI.InvokeReset();

        this.gameObject.SetActive(false);
    }
}
