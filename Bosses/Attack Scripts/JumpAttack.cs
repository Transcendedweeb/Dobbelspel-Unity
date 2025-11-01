using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [Header("Object References")]
    public GameObject mainParent;
    public GameObject player;
    public GameObject playerMarker;

    [Header("Effect Settings")]
    public GameObject prefabEffect;
    public Vector3 positionOffsetEffect;

    [Header("Attack Settings")]
    public GameObject prefabAttack;
    public Vector3 positionOffsetAttack;
    public Vector3 positionOffsetRotationAttack;

    [Header("Movement Settings")]
    public float jumpSpeed = 5f;
    public float jumpDistance = 5f;
    public float dashSpeed = 5f;
    public float markerDistance = 7f;
    public float attackDistance = 5f;

    [Header("Animation Settings")]
    public string animTriggerJump = "";
    public string animTriggerAttack = "";
    public float animWaitTime = .1f;
    public float endWaitTime = 0f;

    [Header("Behavior Options")]
    public bool quickReset = false;

    // Private fields
    bool changeMarker = false;
    Animator animator;
    BossAI bossAI;
    float groundY;
    GameObject createdEffect;

    void OnEnable()
    {
        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();
        groundY = mainParent.transform.position.y;

        if (quickReset) bossAI.InvokeReset();

        playerMarker.SetActive(false);

        if (animTriggerJump != "")
            animator.SetTrigger(animTriggerJump);

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        // Jumping upwards
        while (mainParent.transform.position.y < groundY + jumpDistance)
        {
            Vector3 direction = new Vector3(0, 1, 0);
            mainParent.transform.position += direction * jumpSpeed * Time.deltaTime;
            yield return null;
        }

        // Preparing for dash
        playerMarker.SetActive(true);
        playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
        playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();

        if (prefabEffect != null) CreateEffect();

        // Dashing towards the player
        while (true)
        {
            Vector3 direction = (player.transform.position - mainParent.transform.position).normalized;
            mainParent.transform.position += direction * dashSpeed * Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(mainParent.transform.position, player.transform.position);

            if (!changeMarker && distanceToPlayer <= markerDistance)
            {
                changeMarker = true;
                playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
                playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
            }

            if (distanceToPlayer <= attackDistance) break;
            yield return null;
        }

        // Trigger attack
        changeMarker = false;

        if (animTriggerAttack != "")
            animator.SetTrigger(animTriggerAttack);

        if (prefabAttack != null)
            Invoke("Attack", animWaitTime);
    }

    void CreateEffect()
    {
        Vector3 spawnPosition = mainParent.transform.position + positionOffsetEffect;
        Vector3 directionToPlayer = (player.transform.position - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        createdEffect = Instantiate(prefabEffect, spawnPosition, lookRotation, mainParent.transform);
    }

    void Attack()
    {
        // If prefabEffect was created, destroy it
        if (createdEffect != null)
        {
            Destroy(createdEffect);
        }

        // Force the mainParent to return to the ground
        Vector3 groundPosition = new Vector3(mainParent.transform.position.x, groundY, mainParent.transform.position.z);
        mainParent.transform.position = groundPosition;

        playerMarker.SetActive(false);

        // Spawn the attack prefab with position and rotation offsets
        Vector3 spawnPosition = gameObject.transform.position + positionOffsetAttack;
        Vector3 directionToPlayer = (player.transform.position - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(positionOffsetRotationAttack);

        Instantiate(prefabAttack, spawnPosition, lookRotation);

        // Wait before ending
        Invoke("End", endWaitTime);
    }

    void End()
    {
        if (!quickReset) bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
