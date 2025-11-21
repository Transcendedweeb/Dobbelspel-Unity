using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(PcShoot))]
public class DodgeRoll : MonoBehaviour
{
    [Header("Dodge settings")]
    public float rollDistance = 5f;
    public float rollSpeed = 10f;
    public float resetDelay = .5f;
    public float animationDelay = .1f;

    [Header("Dodge animations")]
    public GameObject model;
    public Animator animator;
    [HideInInspector] public enum DashState { forward = 1, backwards = 2, left = 3, right = 4, reset = 5 };

    [Header("Dodge particles")]
    public List<GameObject> dashParticlesUp = new();
    public List<GameObject> dashParticlesDown = new();
    public List<GameObject> dashParticlesLeft = new();
    public List<GameObject> dashParticlesRight = new();
    public PlayerMovementParticles playerMovementParticles;

    [Header("Dodge sounds")]
    public AudioSource audioSource;
    public AudioClip dodgeSFX;

    [Header("Energy settings")]
    public bool dodgingCostEnergy = false;
    public PlayerEnergy playerEnergy;
    public float energyCost = 10f;

    bool trigger;
    bool rolling = false;
    PcMovement pcMovement;
    CharacterController characterController;

    void Start()
    {
        trigger = GetComponent<PcShoot>().triggerRelease;
        pcMovement = GetComponent<PcMovement>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();

            HandleDodgingLogics();
        }
    }

    void HandleDodgingLogics()
    {
        if (!trigger && !rolling)
        {
            if (dodgingCostEnergy)
            {
                if (!playerEnergy.LowerEnergy(energyCost))
                {
                    return;
                }
            }

            rolling = true;
            pcMovement.enabled = false;
            animator.SetInteger("Lean position", 5);
            GetComponent<PcShoot>().enabled = false;
            GetComponent<HealthManager>().enabled = false;
            Roll();
        }
    }

    void Reset()
    {
        ArduinoDataManager.Instance.ResetButtonStates();
        pcMovement.enabled = true;
        rolling = false;
        animator.SetInteger("Dash position", (int)DashState.reset);
        GetComponent<PcShoot>().enabled = true;
        GetComponent<HealthManager>().enabled = true;
        model.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void Roll()
    {
        Vector3 rollDirection;
        DashState newDashPosition;

        switch (pcMovement.lastMov)
        {
            case "RightUp":
                rollDirection = (transform.right + transform.forward).normalized;
                newDashPosition = DashState.forward;
                StartCoroutine(StartParticleBoost(dashParticlesUp));
                break;

            case "LeftUp":
                rollDirection = (-transform.right + transform.forward).normalized;
                newDashPosition = DashState.forward;
                StartCoroutine(StartParticleBoost(dashParticlesUp));
                break;

            case "RightDown":
                rollDirection = (transform.right - transform.forward).normalized;
                newDashPosition = DashState.backwards;
                StartCoroutine(StartParticleBoost(dashParticlesDown));
                break;

            case "LeftDown":
                rollDirection = (-transform.right - transform.forward).normalized;
                newDashPosition = DashState.backwards;
                StartCoroutine(StartParticleBoost(dashParticlesDown));
                break;

            case "Down":
                rollDirection = -transform.forward;
                newDashPosition = DashState.backwards;
                StartCoroutine(StartParticleBoost(dashParticlesDown));
                break;

            case "Left":
                rollDirection = -transform.right;
                newDashPosition = DashState.left;
                StartCoroutine(StartParticleBoost(dashParticlesLeft));
                break;

            case "Right":
                rollDirection = transform.right;
                newDashPosition = DashState.right;
                StartCoroutine(StartParticleBoost(dashParticlesRight));
                break;

            default:
                rollDirection = transform.forward;
                newDashPosition = DashState.forward;
                playerMovementParticles.EnableParticleEffect(dashParticlesUp, false);
                StartCoroutine(StartParticleBoost(dashParticlesUp));
                break;
        }

        PlaySfx.PlaySFX(dodgeSFX, audioSource);
        animator.SetInteger("Dash position", (int)newDashPosition);

        FindObjectOfType<PlayerCameraHandler>()?.TriggerDodgeLag();
        FindObjectOfType<PlayerCameraHandler>()?.TriggerCameraShake(1.5f, 1.2f);

        if (rollDirection != Vector3.zero)
        {
            StartCoroutine(PerformRoll(rollDirection));
        }

        if (rollDirection != Vector3.zero)
        {
            StartCoroutine(PerformRoll(rollDirection));
        }
    }

    IEnumerator PerformRoll(Vector3 rollDirection)
    {
        float distanceRolled = 0f;

        while (distanceRolled < rollDistance)
        {
            model.transform.localRotation = Quaternion.Euler(0, 180, 0);
            float step = rollSpeed * Time.deltaTime;
            characterController.Move(rollDirection * step);
            distanceRolled += step;
            yield return null;
        }
        Invoke(nameof(Reset), resetDelay);
    }

    IEnumerator StartParticleBoost(List<GameObject> particlesToEnable)
    {
        yield return new WaitForSeconds(animationDelay);
        playerMovementParticles.EnableParticleEffect(particlesToEnable, false);
    }
}
