using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject nextCamera;
    public bool changeOnEnable = false;

    public void ChangeToNextCamera()
    {
        nextCamera.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ChangeToSpecificCam(GameObject newCam)
    {
        newCam.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void DisableThisScript()
    {
        this.enabled = false;
    }

    void OnEnable()
    {
        if (changeOnEnable)
        {
            ChangeToNextCamera();
            DisableThisScript();
        }
    }
}
