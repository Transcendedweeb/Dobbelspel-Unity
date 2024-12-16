using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonInArea : MonoBehaviour
{
    public GameObject boss;
    public GameObject prefab;
    public Transform center;
    public float initialWaitTime = 0f;
    public int projectileCount = 1;
    public float cooldown = 0f;
    public Vector2 areaSize = new Vector2(5f, 5f);
    public float fixedYPosition = 0f;
    public string animTriggerName = "";
    public float finalWaitTime = 0f;
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
        if (animTriggerName != "") animator.SetTrigger(animTriggerName);
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

        if (!quickReset) bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 centerPosition = center != null ? center.position : transform.position;
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        return new Vector3(centerPosition.x + randomX, fixedYPosition, centerPosition.z + randomZ);
    }

}
