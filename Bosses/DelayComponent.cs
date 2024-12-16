using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayComponent : MonoBehaviour
{
    public Behaviour behaviour;
    public MeshRenderer meshRenderer;
    public float delay;

    void OnEnable()
    {
        Invoke("EnableComponent", delay);
    }

    void EnableComponent()
    {
        if (behaviour != null) behaviour.enabled = true;
        if (meshRenderer != null) meshRenderer.enabled = true;
    }
}
