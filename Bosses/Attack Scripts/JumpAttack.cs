using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
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

    [Header("Advanced Dash Behavior")]
    public bool useLastKnownPosition = false;
    public Vector3 lastPositionOffset;

    bool changeMarker = false;
    Animator animator;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;
    float groundY;
    GameObject createdEffect;
    Vector3 lastKnownPosition;
    GameObject mainParent;


    void OnEnable()
    {
        mainParent = transform.root.gameObject;

        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();
        playerRefProvider = mainParent.GetComponent<PlayerReferenceProvider>();
        groundY = mainParent.transform.position.y;

        if (quickReset) bossAI.InvokeReset();

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
            playerMarker.SetActive(false);

        if (animTriggerJump != "")
            animator.SetTrigger(animTriggerJump);

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        while (mainParent.transform.position.y < groundY + jumpDistance)
        {
            Vector3 direction = new Vector3(0, 1, 0);
            mainParent.transform.position += direction * jumpSpeed * Time.deltaTime;
            yield return null;
        }

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
        {
            playerMarker.SetActive(true);
            playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
            playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
        }

        if (prefabEffect != null) CreateEffect();

        if (useLastKnownPosition)
        {
            lastKnownPosition = playerRefProvider.GetPlayerPosition() + lastPositionOffset;
        }

        while (true)
        {
            Vector3 targetPosition;

            if (useLastKnownPosition)
                targetPosition = lastKnownPosition;
            else
                targetPosition = playerRefProvider.GetPlayerPosition();

            Vector3 direction = (targetPosition - mainParent.transform.position).normalized;
            mainParent.transform.position += direction * dashSpeed * Time.deltaTime;

            float distanceToTarget = Vector3.Distance(mainParent.transform.position, targetPosition);

            if (!changeMarker && distanceToTarget <= markerDistance)
            {
                changeMarker = true;
                if (playerMarker != null)
                {
                    playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
                    playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
                }
            }

            if (distanceToTarget <= attackDistance)
                break;

            yield return null;
        }

        changeMarker = false;

        if (animTriggerAttack != "")
            animator.SetTrigger(animTriggerAttack);

        if (prefabAttack != null)
            Invoke(nameof(Attack), animWaitTime);
    }

    void CreateEffect()
    {
        Vector3 spawnPosition = mainParent.transform.position + positionOffsetEffect;
        Vector3 playerPos = playerRefProvider.GetPlayerPosition();
        Vector3 directionToPlayer = (playerPos - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        createdEffect = Instantiate(prefabEffect, spawnPosition, lookRotation, mainParent.transform);
    }

    void Attack()
    {
        if (createdEffect != null)
        {
            Destroy(createdEffect);
        }

        Vector3 groundPosition = new Vector3(mainParent.transform.position.x, groundY, mainParent.transform.position.z);
        mainParent.transform.position = groundPosition;

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
            playerMarker.SetActive(false);

        Vector3 spawnPosition = gameObject.transform.position + positionOffsetAttack;
        Vector3 playerPos = playerRefProvider.GetPlayerPosition();
        Vector3 directionToPlayer = (playerPos - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(positionOffsetRotationAttack);

        Instantiate(prefabAttack, spawnPosition, lookRotation);

        Invoke("End", endWaitTime);
    }

    void End()
    {
        if (!quickReset) bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
