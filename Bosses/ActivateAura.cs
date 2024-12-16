using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAura : MonoBehaviour
{
    public GameObject[] auras;
    public float enableTime = 0f;
    public float disableTime = 0f;

    void OnEnable()
    {
        Invoke("EnableAuras", enableTime);
        if (disableTime != 0f) Invoke("DeactivateAuras", disableTime);
    }

    void EnableAuras()
    {
        foreach (GameObject aura in auras)
        {
            aura.SetActive(true);
        }
    }

    public void DeactivateAuras()
    {
        foreach (GameObject aura in auras)
        {
            aura.SetActive(false);
        }
    }
}
