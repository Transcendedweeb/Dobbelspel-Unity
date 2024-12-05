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
    [HideInInspector] public bool attacking = true;
    int count;
    bool specialActivated = false;
    HealthManager healthManager;

    void Start()
    {
        InvokeReset();
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
                    Debug.LogError("Health boss under 50%. Special attack!");
                    specialActivated = true;
                    special.SetActive(true);
                    attacking = true;
                    return;
                }
            }

            if (distanceToPlayer <= closeDistance && melee != null)
            {
                Debug.Log("Player is close. Boss is attacking!");
                melee.SetActive(true);
            }
            else
            {
                Debug.Log("Normal attack inbound!");
                if (count >= attacks.Length) count = 0;

                attacks[count].SetActive(true);
                count++;
            }
            attacking = true;
        }

    }

    void ResetAttacking()
    {
        Debug.LogWarning("Reset attacking");
        attacking = false;
    }

    public void InvokeReset()
    {
        Invoke("ResetAttacking", attackCooldown);
    }
}

