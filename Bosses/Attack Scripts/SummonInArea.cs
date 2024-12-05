using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonInArea : MonoBehaviour
{
    public GameObject prefab;
    public BossAI bossAI;
    public float initialWaitTime = 0f;
    public int projectileCount = 1;
    public float cooldown = 0f;
    public Vector2 areaSize = new Vector2(5f, 5f);
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
            Vector3 randomPosition = GetRandomPosition();
            
            Instantiate(prefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(cooldown);
        }

        bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);

        return new Vector3(transform.position.x + randomX, fixedYPosition, transform.position.z + randomZ);
    }
}
