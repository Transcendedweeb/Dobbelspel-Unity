using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20f;

    void Update()
    {
        if (!string.IsNullOrEmpty(ArduinoDataManager.Instance.JoystickDirection))
        {
            MoveCamera(ArduinoDataManager.Instance.JoystickDirection);
            ArduinoDataManager.Instance.ResetButtonStates();
        }
    }

    void MoveCamera(string direction)
    {
        Vector3 moveVector = Vector3.zero;

        switch (direction)
        {
            case "Left":
                moveVector = Vector3.left;
                break;
            case "Right":
                moveVector = Vector3.right;
                break;
            case "Up":
                moveVector = Vector3.up;
                break;
            case "Down":
                moveVector = Vector3.down;
                break;
        }

        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
    }
}
