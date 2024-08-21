using UnityEngine;

public class ArduinoDataManager : MonoBehaviour
{
    public static ArduinoDataManager Instance { get; private set; }

    public bool ButtonAPressed { get; set; }
    public bool ButtonBPressed { get; set; }
    public bool JoystickButtonPressed { get; set; }
    public string JoystickDirection { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetButtonState(string message)
    {
        if (message == "A")
        {
            ButtonAPressed = true;
        }
        else if (message == "B")
        {
            ButtonBPressed = true;
        }
        else if (message == "Joystick Button Pressed")
        {
            JoystickButtonPressed = true;
        }
        else if (message == "Left" || message == "Right" || message == "Up" || message == "Down")
        {
            JoystickDirection = message;
        }
    }

    public void ResetButtonStates()
    {
        ButtonAPressed = false;
        ButtonBPressed = false;
        JoystickButtonPressed = false;
        JoystickDirection = null;
    }
}
