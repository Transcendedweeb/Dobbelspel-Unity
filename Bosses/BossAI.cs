using System.Collections;
using UnityEngine;

[System.Serializable]
public class AttackData
{
    public GameObject attackObject;
    public float cooldownAfter = 1f;
}

[System.Serializable]
public class AttackSequence
{
    public string name;
    public float startCooldown = 0f;
    public AttackData[] attacks;
}

[System.Serializable]
public class SpecialAttack
{
    public string name;
    public AttackData[] attacks;
    public float[] hpThresholds;
    [HideInInspector] public bool[] triggered;
}

[RequireComponent(typeof(HealthManager))]
public class BossAI : MonoBehaviour
{
    [Header("Attack Settings")]
    public AttackSequence[] attackSequences;
    public SpecialAttack specialAttack;
    public GameObject melee;
    public GameObject player;
    public float closeDistance = 5f;
    public float startWaitTime = 0f;

    [HideInInspector] public bool attacking = true;

    private int currentSequenceIndex = 0;
    private int currentAttackIndex = 0;
    private HealthManager healthManager;
    private bool sequenceStarting = false;
    private AttackData currentAttack;

    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        if (specialAttack != null)
        {
            specialAttack.triggered = new bool[specialAttack.hpThresholds.Length];
        }
        Invoke(nameof(InvokeReset), startWaitTime);
    }

    void Update()
    {
        if (!attacking && !sequenceStarting)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (specialAttack != null)
            {
                for (int i = 0; i < specialAttack.hpThresholds.Length; i++)
                {
                    if (!specialAttack.triggered[i] && healthManager.health <= specialAttack.hpThresholds[i])
                    {
                        specialAttack.triggered[i] = true;
                        StartCoroutine(PerformAttackSequence(specialAttack.attacks));
                        return;
                    }
                }
            }

            if (distanceToPlayer <= closeDistance && melee != null)
            {
                melee.SetActive(true);
                attacking = true;
            }
            else if (attackSequences.Length > 0)
            {
                AttackSequence sequence = attackSequences[currentSequenceIndex];

                if (currentAttackIndex >= sequence.attacks.Length)
                {
                    currentAttackIndex = 0;

                    if (attackSequences.Length > 1)
                    {
                        int nextIndex;
                        do
                        {
                            nextIndex = Random.Range(0, attackSequences.Length);
                        } while (nextIndex == currentSequenceIndex);

                        currentSequenceIndex = nextIndex;
                    }

                    return;
                }

                currentAttack = sequence.attacks[currentAttackIndex];
                currentAttackIndex++;
                attacking = true;
                currentAttack.attackObject.SetActive(true);
            }
        }
    }

    IEnumerator PerformAttackSequence(AttackData[] sequence)
    {
        foreach (AttackData attack in sequence)
        {
            currentAttack = attack;
            attack.attackObject.SetActive(true);
            attacking = true;

            yield return new WaitUntil(() => !attacking);

            yield return new WaitForSeconds(attack.cooldownAfter);
        }

        InvokeReset();
    }

    public void InvokeReset()
    {
        if (currentAttack != null)
        {
            StartCoroutine(WaitAfterAttack(currentAttack.cooldownAfter));
        }
        else
        {
            attacking = false;
        }
    }

    IEnumerator WaitAfterAttack(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        attacking = false;
    }
}
