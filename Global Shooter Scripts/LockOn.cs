using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [Header("Target Settings")]
    public GameObject target;

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;
    public Vector3 rotationOffset = Vector3.zero;
    public bool setXRotation = false;

    [Header("Clamp Settings")]
    public bool clampXRotation = false;
    public float minXAngle = -30f;
    public float maxXAngle = 45f;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (direction.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(rotationOffset);

            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (setXRotation)
            {
                Vector3 euler = smoothedRotation.eulerAngles;

                if (euler.x > 180) euler.x -= 360f;

                if (clampXRotation)
                    euler.x = Mathf.Clamp(euler.x, minXAngle, maxXAngle);

                transform.rotation = Quaternion.Euler(euler);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, smoothedRotation.eulerAngles.y, 0f);
            }
        }
    }
}
