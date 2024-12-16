using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndAttack : MonoBehaviour
{
    public GameObject mainParent;
    public GameObject prefab;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public GameObject player;
    public GameObject playerMarker;
    public float dashSpeed = 5f;
    public float markerDistance = 7f;
    public float attackDistance = 5f;
    public string animTriggerName = "";
    public float animWaitTime = .1f;
    public float endWaitTime = 0f;
    public bool quickReset = false;
    bool changeMarker = false;
    Animator animator;
    BossAI bossAI;

    void OnEnable()
    {
        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();

        if (quickReset) bossAI.InvokeReset();
        playerMarker.SetActive(true);
        animator.SetTrigger("Dash");
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
        playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
        while (true)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            mainParent.transform.position += direction * dashSpeed * Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); 
            if (!changeMarker && distanceToPlayer <= markerDistance)
            {
                changeMarker = true;
                playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
                playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
            }
            if (distanceToPlayer <= attackDistance) break;
            yield return null;
        }
        changeMarker = false;
        if (animTriggerName != "") animator.SetTrigger(animTriggerName);
        if (prefab != null) Invoke("Attack", animWaitTime);
        else
        {
            yield return new WaitForSeconds(animWaitTime);
            End();
        }
    }

    void Attack()
    {
        playerMarker.SetActive(false);
        Vector3 spawnPosition = gameObject.transform.position + positionOffset;
        Vector3 directionToPlayer = (player.transform.position - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(rotationOffset);
        Instantiate(prefab, spawnPosition, lookRotation);
        Invoke("End", endWaitTime);
    }

    void End()
    {
        if (!quickReset) bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
