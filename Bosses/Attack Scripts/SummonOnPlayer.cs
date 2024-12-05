using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOnPlayer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;
    public BossAI bossAI;
    public float initialWaitTime = 0f;
    public int projectileCount = 1;
    public float cooldown = 0f;
    public float fixedYPosition = 0f;

    void OnEnable()
    {
        Invoke(nameof(StartCoroutineCall), initialWaitTime);
    }

    void StartCoroutineCall()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
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

        bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
