using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float timeInSeconds = 0f;
    public bool destroyOnEnable = false;

    void OnEnable()
    {
        if (destroyOnEnable) Invoke("DestroySelfFunc", timeInSeconds);
    }

    public void DestroySelfFunc()
    {
        Destroy(gameObject);
    }
}
