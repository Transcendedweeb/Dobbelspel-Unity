using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public bool destroyOnEnable = false;
    public bool destroyOnStart = false;
    public float timeInSeconds = 0f;

    void OnEnable()
    {
        if (destroyOnEnable) Invoke(nameof(DestroySelfFunc), timeInSeconds);
    }
    
    void Start()
    {
        if (destroyOnStart) Invoke(nameof(DestroySelfFunc), timeInSeconds);
    }

    public void DestroySelfFunc()
    {
        Destroy(gameObject);
    }
}
