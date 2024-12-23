using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleConsole : MonoBehaviour
{
    public TMP_Text consoleText;
    void Update()
    {
        if (!string.IsNullOrEmpty(ArduinoDataManager.Instance.JoystickDirection))
        {
            if (
                ArduinoDataManager.Instance.JoystickDirection  == "LeftUp" ||
                ArduinoDataManager.Instance.JoystickDirection  == "LeftDown" ||
                ArduinoDataManager.Instance.JoystickDirection  == "Left" ||
                ArduinoDataManager.Instance.JoystickDirection  == "RightUp" ||
                ArduinoDataManager.Instance.JoystickDirection  == "RightDown" ||
                ArduinoDataManager.Instance.JoystickDirection  == "Right" ||
                ArduinoDataManager.Instance.JoystickDirection  == "Up" ||
                ArduinoDataManager.Instance.JoystickDirection  == "Down")
            {
                switch (ArduinoDataManager.Instance.JoystickDirection)
                {
                    case "LeftUp":
                        consoleText.text = "LeftUp";
                        break;
                    case "LeftDown":
                        consoleText.text = "LeftDown";
                        break;
                    case "Left":
                        consoleText.text = "Left";
                        break;
                    case "RightUp":
                        consoleText.text = "RightUp";
                        break;
                    case "RightDown":
                        consoleText.text = "RightDown";
                        break;
                    case "Right":
                        consoleText.text = "Right";
                        break;
                    case "Up":
                        consoleText.text = "Up";
                        break;
                    case "Down":
                        consoleText.text = "Down";
                        break;
                }
                ArduinoDataManager.Instance.ResetButtonStates();
            }
        }
    
        else if (ArduinoDataManager.Instance.ButtonAPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            consoleText.text = "A";
        }

        else if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            consoleText.text = "B";
        }

        else if (ArduinoDataManager.Instance.ButtonCPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            consoleText.text = "C";
        }

        else if (ArduinoDataManager.Instance.ButtonDPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            consoleText.text = "D";
        }

        else if (ArduinoDataManager.Instance.ButtonEPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            consoleText.text = "E";
        }
    }
}
