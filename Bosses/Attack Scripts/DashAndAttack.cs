using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndAttack : MonoBehaviour
{
    [Header("Object References")]
    public GameObject prefab;

    [Header("Position & Rotation Offsets")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    [Header("Dash Settings")]
    public float dashSpeed = 5f;
    public float markerDistance = 7f;
    public float attackDistance = 5f;

    [Header("Animation Settings")]
    public string animTriggerName = "";
    public float animWaitTime = .1f;
    public float endWaitTime = 0f;

    [Header("Behavior Options")]
    public bool quickReset = false;

    // Private fields
    bool changeMarker = false;
    Animator animator;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;
    GameObject mainParent;


    void OnEnable()
    {
        mainParent = transform.root.gameObject;

        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();
        playerRefProvider = mainParent.GetComponent<PlayerReferenceProvider>();

        if (quickReset) bossAI.InvokeReset();
        
        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
            playerMarker.SetActive(true);
        
        animator.SetTrigger("Dash");
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
        {
            playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
            playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
        }

        while (true)
        {
            Vector3 playerPos = playerRefProvider.GetPlayerPosition();
            Vector3 direction = (playerPos - transform.position).normalized;
            mainParent.transform.position += direction * dashSpeed * Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, playerPos);

            if (!changeMarker && distanceToPlayer <= markerDistance)
            {
                changeMarker = true;
                if (playerMarker != null)
                {
                    playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
                    playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
                }
            }

            if (distanceToPlayer <= attackDistance) break;
            yield return null;
        }

        changeMarker = false;

        if (animTriggerName != "")
            animator.SetTrigger(animTriggerName);

        if (prefab != null)
            Invoke("Attack", animWaitTime);
        else
        {
            yield return new WaitForSeconds(animWaitTime);
            End();
        }
    }

    void Attack()
    {
        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
            playerMarker.SetActive(false);

        Vector3 spawnPosition = gameObject.transform.position + positionOffset;
        Vector3 playerPos = playerRefProvider.GetPlayerPosition();
        Vector3 directionToPlayer = (playerPos - spawnPosition).normalized;
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
