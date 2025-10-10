using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    public float timeInSeconds;
    public bool disableOnEnable = true;

    void OnEnable()
    {
        if (disableOnEnable) Invoke(nameof(DisableSelfFunc), timeInSeconds);
    }

    public void DisableSelfFunc()
    {
        gameObject.SetActive(false);
    }
}
