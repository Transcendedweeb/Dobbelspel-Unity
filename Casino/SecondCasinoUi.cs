using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCasinoUi : MonoBehaviour
{
    public GameObject mainGo;
    public List<GameObject> scrollableList;
    public float scrollDelay = 0.2f;
    int count = 0;
    bool cooldown = false;
    
    void Update()
    {
        string joystickDirection = ArduinoDataManager.Instance.JoystickDirection;
        if (joystickDirection == "Down")
        {
            if (count < 3 && !cooldown)
            {
                ChangeColor(Color.white);
                count++;
                ChangeColor(Color.red);

                cooldown = true;
                Invoke("DisableCooldown", scrollDelay);
            }
            ResetJoystick();
        }
        else if (joystickDirection == "Up")
        {
            if (count > 0 && !cooldown)
            {
                ChangeColor(Color.white);
                count--;
                ChangeColor(Color.red);

                cooldown = true;
                Invoke("DisableCooldown", scrollDelay);
            }
            ResetJoystick();
        }
        else if (ArduinoDataManager.Instance.ButtonAPressed)
        {
            mainGo.GetComponent<RollCasinoReward>().CheckVictroy(count);
        }
    }

    void OnEnable()
    {
        ChangeColor(Color.red);
    }

    void DisableCooldown()
    {
        cooldown = false;
    }

    void ChangeColor(Color color)
    {
        scrollableList[count].GetComponent<Image>().color = color;
    }

    void ResetJoystick()
    {
        ArduinoDataManager.Instance.JoystickButtonPressed = false;
        ArduinoDataManager.Instance.JoystickDirection = null;
    }
}
