using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public PcMovement playerMovement;

    [Header("Camera Lag Settings")]
    public float followSpeed = 10f;        // How fast camera catches up
    public float strafeLag = 0.5f;         // How much it lags when moving left/right
    public float forwardLag = 0.3f;        // How much it lags when moving forward/back
    public float returnSpeed = 5f;         // How fast it recenters after movement stops

    private Vector3 desiredLocalPos;
    private Vector3 initialLocalPos;
    private Vector3 velocity;

    void Start()
    {
        if (target == null)
            target = transform.parent;

        initialLocalPos = transform.localPosition;
        desiredLocalPos = initialLocalPos;
    }

    void LateUpdate()
    {
        if (playerMovement == null)
            return;

        string dir = playerMovement.lastMov;
        Vector3 lagOffset = Vector3.zero;

        switch (dir)
        {
            case "Left":
            case "LeftUp":
            case "LeftDown":
                lagOffset += Vector3.right * strafeLag;
                break;

            case "Right":
            case "RightUp":
            case "RightDown":
                lagOffset += Vector3.left * strafeLag;
                break;
        }

        switch (dir)
        {
            case "Up":
            case "RightUp":
            case "LeftUp":
                lagOffset += Vector3.back * forwardLag;
                break;

            case "Down":
            case "RightDown":
            case "LeftDown":
                lagOffset += Vector3.forward * forwardLag;
                break;
        }

        desiredLocalPos = initialLocalPos + lagOffset;

        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            desiredLocalPos,
            ref velocity,
            1f / followSpeed
        );

        if (dir == "None")
            desiredLocalPos = initialLocalPos;
    }
}
