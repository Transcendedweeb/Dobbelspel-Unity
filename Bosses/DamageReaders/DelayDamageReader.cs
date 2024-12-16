using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDamageReader : MonoBehaviour
{
    public float delay = 1f;
    public new Collider collider;

    void OnEnable()
    {
        Invoke("SetComponent", delay);
    }

    void SetComponent()
    {
        collider.enabled = true;
    }
}
