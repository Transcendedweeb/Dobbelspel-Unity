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

    [Header("Dodge Lag Settings")]
    public float dodgeLagMultiplier = 2f;     // How much to amplify lag during a dodge
    public float dodgeLagDuration = 0.5f;     // How long to keep that extra lag
    private bool isDodging = false;     // Flag

    [Header("Camera Shake Settings")]
    public float shakeIntensity = 0.1f;    // Default strength of the shake
    public float shakeDuration = 0.2f;     // Default duration of the shake
    public AnimationCurve shakeFalloff = AnimationCurve.EaseInOut(0, 1, 1, 0); // Smooth fade-out

    Vector3 desiredLocalPos;
    Vector3 initialLocalPos;
    Vector3 velocity;
    Coroutine shakeRoutine;

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

    public void TriggerDodgeLag()
    {
        if (!isDodging) StartCoroutine(ApplyDodgeLag());
    }

    private IEnumerator ApplyDodgeLag()
    {
        isDodging = true;
        float originalStrafeLag = strafeLag;
        float originalForwardLag = forwardLag;

        strafeLag *= dodgeLagMultiplier;
        forwardLag *= dodgeLagMultiplier;

        yield return new WaitForSeconds(dodgeLagDuration);

        strafeLag = originalStrafeLag;
        forwardLag = originalForwardLag;
        isDodging = false;
    }

    public void TriggerCameraShake(float intensityMultiplier = 1f, float durationMultiplier = 1f)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(DoCameraShake(intensityMultiplier, durationMultiplier));
    }

    IEnumerator DoCameraShake(float intensityMultiplier, float durationMultiplier)
    {
        float elapsed = 0f;
        Vector3 originalLocalPos = transform.localPosition;

        while (elapsed < shakeDuration * durationMultiplier)
        {
            float normalizedTime = elapsed / (shakeDuration * durationMultiplier);
            float falloff = shakeFalloff.Evaluate(normalizedTime);
            float currentIntensity = shakeIntensity * intensityMultiplier * falloff;

            // Random offset (small sphere)
            Vector3 shakeOffset = Random.insideUnitSphere * currentIntensity;

            transform.localPosition = desiredLocalPos + shakeOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Smoothly return to the normal desired position
        float t = 0f;
        while (t < 0.1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, desiredLocalPos, t / 0.1f);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = desiredLocalPos;
    }
}
