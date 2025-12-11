using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public GameObject mainParent;
    public GameObject player;
    public GameObject prefab;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public ActivateAura activateAura;
    public GameObject playerMarker;
    public Transform jumpLocation;
    public Transform arcTowards;
    public string animTriggerHold = "";
    public string animTriggerJump = "";
    public string animTriggerAttack = "";
    public float holdTime = 2f;
    public float jumpAnimTime = .2f;
    public float endWaitTime = 4f;
    public float endTime = 3f;
    public float arcSpeed = 10f;
    public float chargeSpeed = 10f;
    public float reactionDistance = 10f;
    public float attackDistance = 5f;
    Animator animator;
    BossAI bossAI;
    float groundY;
    bool changeMarker = false;

    void OnEnable()
    {
        animator = mainParent.GetComponent<Animator>();
        bossAI = mainParent.GetComponent<BossAI>();
        groundY = mainParent.transform.position.y;

        if (animTriggerHold != "")
            animator.SetTrigger(animTriggerHold);

        Invoke("SetCoroutine", holdTime);
    }

    void SetCoroutine()
    {
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        if (animTriggerJump != "")
            animator.SetTrigger(animTriggerJump);

        yield return new WaitForSeconds(jumpAnimTime);
        Teleport();

        while (Vector3.Distance(mainParent.transform.position, arcTowards.position) > 0.1f)
        {
            Vector3 direction = (arcTowards.position - mainParent.transform.position).normalized;
            mainParent.transform.position += direction * arcSpeed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            mainParent.transform.rotation = Quaternion.Slerp(mainParent.transform.rotation, lookRotation, Time.deltaTime * arcSpeed);

            yield return null;
        }

        playerMarker.SetActive(true);
        playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
        playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();

        while (true)
        {
            Vector3 direction = (player.transform.position - mainParent.transform.position).normalized;
            mainParent.transform.position += direction * chargeSpeed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            mainParent.transform.rotation = Quaternion.Slerp(mainParent.transform.rotation, lookRotation, Time.deltaTime * chargeSpeed);

            float distanceToPlayer = Vector3.Distance(mainParent.transform.position, player.transform.position);

            if (!changeMarker && distanceToPlayer <= reactionDistance)
            {
                changeMarker = true;
                playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
                playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
            }

            if (distanceToPlayer <= attackDistance)
            {
                if (animTriggerAttack != "")
                    animator.SetTrigger(animTriggerAttack);

                break;
            }

            yield return null;
        }
        activateAura.DeactivateAuras();
        Attack();
        Vector3 groundPosition = new Vector3(mainParent.transform.position.x, groundY, mainParent.transform.position.z);
        mainParent.transform.position = groundPosition;
        Invoke("End", endWaitTime);
    }

    void Teleport()
    {
        mainParent.transform.position = jumpLocation.position;
    }

    void Attack()
    {
        playerMarker.SetActive(false);
        Vector3 spawnPosition = gameObject.transform.position + positionOffset;
        Vector3 directionToPlayer = (player.transform.position - spawnPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(rotationOffset);
        Instantiate(prefab, spawnPosition, lookRotation);
    }

    void End()
    {
        bossAI.currentAttack = null;
        bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
