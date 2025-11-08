using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PcMovement : MonoBehaviour
{
    [Header("Movement settings")]
    public float moveSpeed = 10f;
    public float gravity = -9.8f;
    [HideInInspector] public string lastMov;

    [Header("Animation setting")]
    public Animator modelAnim;
    [HideInInspector]  public enum MovementState {idle = 0, leanForward = 1, leanBackwards = 2, leanLeft = 3, leanRight = 4};

    [Header("Sound settings")]
    public AudioSource movingAudioSource; // constantly loops the audio clip when moving, and only stops when the player stops moving
    public AudioClip movingSFX;

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
        Vector3 newMoveDir;
        MovementState newLeanPosition;

        switch (direction)
        {
            case "RightUp":
                newMoveDir = (Vector3.forward + Vector3.right).normalized;
                newLeanPosition = MovementState.leanForward;
                break;

            case "LeftUp":
                newMoveDir = (Vector3.forward + Vector3.left).normalized;
                newLeanPosition = MovementState.leanForward;
                break;

            case "RightDown":
                newMoveDir = (Vector3.back + Vector3.right).normalized;
                newLeanPosition = MovementState.leanBackwards;
                break;

            case "LeftDown":
                newMoveDir = (Vector3.back + Vector3.left).normalized;
                newLeanPosition = MovementState.leanBackwards;
                break;

            case "Up":
                newMoveDir = Vector3.forward;
                newLeanPosition = MovementState.leanForward;
                break;

            case "Down":
                newMoveDir = Vector3.back;
                newLeanPosition = MovementState.leanBackwards;
                break;

            case "Left":
                newMoveDir = Vector3.left;
                newLeanPosition = MovementState.leanLeft;
                break;

            case "Right":
                newMoveDir = Vector3.right;
                newLeanPosition = MovementState.leanRight;
                break;

            default:
                moveDirection = Vector3.zero;
                lastMov = "None";
                modelAnim.SetInteger("Lean position", (int)MovementState.idle);
                DisableSFX();
                return;
        }

        if (!movingAudioSource.isPlaying) PlaySfx.PlaySFX(movingSFX, movingAudioSource, loop: true);

        lastMov = direction;
        moveDirection = newMoveDir;

        modelAnim.SetInteger("Lean position", (int)newLeanPosition);
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

    public void DisableLeanPosition()
    {
        modelAnim.SetInteger("Lean position", -1);
        DisableSFX();
    }

    public void DisableSFX()
    {
        PlaySfx.StopSFX(movingAudioSource, movingSFX);
    }
}
