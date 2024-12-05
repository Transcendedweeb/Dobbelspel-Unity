using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed = 5f;
    public Vector3 rotationOffset = Vector3.zero;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(rotationOffset);
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, smoothedRotation.eulerAngles.y, 0f);
        }
    }
}
