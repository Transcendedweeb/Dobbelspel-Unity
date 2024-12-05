using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public float rollDistance = 5f;
    public float rollSpeed = 10f;
    public GameObject model;
    public Animator animator;
    bool trigger;
    bool rolling = false;
    PcMovement pcMovement;
    private CharacterController characterController;

    void Start()
    {
        trigger = this.GetComponent<PcShoot>().triggerRelease;
        pcMovement = this.GetComponent<PcMovement>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();

            if (!trigger && !rolling)
            {
                rolling = true;
                pcMovement.enabled = false;
                this.GetComponent<PcShoot>().enabled = false;
                this.GetComponent<HealthManager>().enabled = false;
                Roll();
            }
        }
    }

    void Reset()
    {
        ArduinoDataManager.Instance.ResetButtonStates();
        pcMovement.enabled = true;
        rolling = false;
        this.GetComponent<PcShoot>().enabled = true;
        this.GetComponent<HealthManager>().enabled = true;
        model.transform.localEulerAngles = new Vector3(5, 12, 0);
    }

    void Roll()
    {
        Vector3 rollDirection = Vector3.zero;
        if (pcMovement.lastMov == "RightUp")
        {
            rollDirection = (transform.right + transform.forward).normalized;
        }
        else if (pcMovement.lastMov == "LeftUp")
        {
            rollDirection = (-transform.right + transform.forward).normalized;
        }
        else if (pcMovement.lastMov == "RightDown")
        {
            rollDirection = (transform.right - transform.forward).normalized;
        }
        else if (pcMovement.lastMov == "LeftDown")
        {
            rollDirection = (-transform.right - transform.forward).normalized;
        }
        else if (pcMovement.lastMov == "Down")
        {
            rollDirection = -transform.forward;
        }
        else if (pcMovement.lastMov == "Left")
        {
            rollDirection = -transform.right;
        }
        else if (pcMovement.lastMov == "Right")
        {
            rollDirection = transform.right;
        }
        else
        {
            rollDirection = transform.forward;
        }

        animator.SetTrigger("Dodge");

        if (rollDirection != Vector3.zero)
        {
            model.transform.rotation = Quaternion.LookRotation(rollDirection);
            StartCoroutine(PerformRoll(rollDirection));
        }
    }

    IEnumerator PerformRoll(Vector3 rollDirection)
    {
        float distanceRolled = 0f;

        while (distanceRolled < rollDistance)
        {
            float step = rollSpeed * Time.deltaTime;
            characterController.Move(rollDirection * step);
            distanceRolled += step;
            yield return null;
        }
        Invoke("Reset", .5f);
    }
}
