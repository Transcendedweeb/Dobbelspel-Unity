using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpark : MonoBehaviour
{
    public float resetTimeInSeconds = .5f;

    void OnEnable()
    {
        Invoke("Reset", resetTimeInSeconds);
    }

    void Reset()
    {
        this.gameObject.SetActive(false);
    }
}
