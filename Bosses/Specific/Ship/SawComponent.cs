using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawComponent : MonoBehaviour
{
    public Animator sawsController;
    public float sawRotation = 5f;

    void OnEnable()
    {
        sawsController.SetTrigger("Open");
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(2.1f + sawRotation);
        sawsController.SetTrigger("Close");        
    }
}
