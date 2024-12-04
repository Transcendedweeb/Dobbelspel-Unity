using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -9.8f;
    [HideInInspector] public string lastMov;
    public Animator modelAnim;
    CharacterController characterController;
    Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateMoveDirection(ArduinoDataManager.Instance.JoystickDirection);
        MoveCharacter();
    }

    void UpdateMoveDirection(string direction)
    {
        moveDirection = Vector3.zero;
        int newForm = 0;

        if (direction == "RightUp")
        {
            moveDirection = (Vector3.forward + Vector3.right).normalized;
            newForm = 3;
        }
        else if (direction == "LeftUp")
        {
            moveDirection = (Vector3.forward + Vector3.left).normalized;
            newForm = 2;
        }
        else if (direction == "RightDown")
        {
            moveDirection = (Vector3.back + Vector3.right).normalized;
            newForm = 3;
        }
        else if (direction == "LeftDown")
        {
            moveDirection = (Vector3.back + Vector3.left).normalized;
            newForm = 2;
        }
        else if (direction == "Up")
        {
            moveDirection = Vector3.forward;
            newForm = 1;
        }
        else if (direction == "Down")
        {
            moveDirection = Vector3.back;
            newForm = 1;
        }
        else if (direction == "Left")
        {
            moveDirection = Vector3.left;
            newForm = 2;
        }
        else if (direction == "Right")
        {
            moveDirection = Vector3.right;
            newForm = 3;
        }
        else
        {
            moveDirection = Vector3.zero;
            modelAnim.SetInteger("Form", 0);
            return;
        }

        lastMov = direction;
        modelAnim.SetInteger("Form", newForm);
    }

    void MoveCharacter()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 relativeMoveDirection = moveDirection.z * transform.forward + moveDirection.x * transform.right;
            relativeMoveDirection.y = gravity;
            characterController.Move(relativeMoveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
