using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject[] attacks;
    public GameObject melee;
    public GameObject special;
    public GameObject player;
    public float closeDistance = 5f;
    public float attackCooldown = 0f;
    public float starWaitTime = 0f;
    [HideInInspector] public bool attacking = true;
    int count;
    bool specialActivated = false;
    HealthManager healthManager;

    void Start()
    {
        Invoke("InvokeReset", starWaitTime);
        count = attacks.Length-1;
        healthManager = this.gameObject.GetComponent<HealthManager>();
    }

    void Update()
    {
        if (!attacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if ((special != null) && !specialActivated)
            {
                if (healthManager.health < (healthManager.maxHealth/2))
                {
                    specialActivated = true;
                    special.SetActive(true);
                    attacking = true;
                    return;
                }
            }

            if (distanceToPlayer <= closeDistance && melee != null)
            {
                melee.SetActive(true);
            }
            else
            {
                if (count >= attacks.Length) count = 0;

                attacks[count].SetActive(true);
                count++;
            }
            attacking = true;
        }

    }

    void ResetAttacking()
    {
        attacking = false;
    }

    public void InvokeReset()
    {
        Invoke("ResetAttacking", attackCooldown);
    }
}

