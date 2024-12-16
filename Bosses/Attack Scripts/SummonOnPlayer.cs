using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOnPlayer : MonoBehaviour
{
    public GameObject boss;
    public GameObject prefab;
    public GameObject player;
    public float initialWaitTime = 0f;
    public int projectileCount = 1;
    public float cooldown = 0f;
    public float fixedYPosition = 0f;
    public string animTriggerName = "";
    public bool quickReset = false;
    BossAI bossAI;
    Animator animator;

    void OnEnable()
    {
        bossAI = boss.GetComponent<BossAI>();
        animator = boss.GetComponent<Animator>();
        if (quickReset) bossAI.InvokeReset();
        Invoke(nameof(StartCoroutineCall), initialWaitTime);
    }

    void StartCoroutineCall()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        if (animTriggerName != "") animator.SetTrigger(animTriggerName);
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

        if (!quickReset) bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
