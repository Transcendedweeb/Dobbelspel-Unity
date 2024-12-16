using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAttack : MonoBehaviour
{
    public float delayTime = 3f;
    public float delayDisable = 6f;
    void OnEnable() 
    {
        Invoke("ActivateChildren", delayTime);
        if (delayDisable != 0f) Invoke("DisableSelf", delayDisable);
    }
    void ActivateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }

    void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
