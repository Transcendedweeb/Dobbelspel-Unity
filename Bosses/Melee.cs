using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public HealthManager healthManagerPlayer;
    public Animator animator;
    public int damage = 100;
    public float waitTime = 1f;
    public BossAI bossAI;

    void OnEnable()
    {
        animator.SetTrigger("Melee");
        Invoke("Reset", waitTime);
    }

    void Reset()
    {
        healthManagerPlayer.AdjustHealth(damage);
        bossAI.InvokeReset();
        this.gameObject.SetActive(false);
    }
}
