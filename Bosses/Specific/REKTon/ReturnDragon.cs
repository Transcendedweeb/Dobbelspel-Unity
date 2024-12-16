using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnDragon : MonoBehaviour
{
    public float timer;
    public Animator animator;
    public string animTriggerName = "";

    void OnEnable()
    {
        Invoke("StartAnimation", timer);
    }

    void StartAnimation()
    {
        animator.SetTrigger(animTriggerName);
    }

    public void DisableSelf()
    {
        GetComponent<ReturnDragon>().enabled = false;
    }
}
