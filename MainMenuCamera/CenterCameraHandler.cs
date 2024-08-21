using System.Collections;
using UnityEngine;
using TMPro;

public class CenterCameraHandler : MonoBehaviour
{
    public GameObject canvasIdle;
    public GameObject canvasMenu;
    public GameObject menuText1;
    public GameObject menuText2;
    int sceneCount = 0;
    int menuIndexCount = 0;
    bool isHandlingButtonPress = false;
    GameObject[] arrayText1;
    GameObject[] arrayText2;
    GameObject selectedGameObject;

    GameObject[] GetChildren(GameObject parent)
    {
        Transform[] childrenTransforms = parent.GetComponentsInChildren<Transform>(true);
        GameObject[] children = new GameObject[childrenTransforms.Length - 1];

        for (int i = 1; i < childrenTransforms.Length; i++)
        {
            children[i - 1] = childrenTransforms[i].gameObject;
        }

        return children;
    }

    void NavigateToMenu()
    {
        Debug.Log("Menu");
        ArduinoDataManager.Instance.ButtonAPressed = false;
        canvasIdle.SetActive(false);
        canvasMenu.SetActive(true);
        sceneCount++;
    }

    void SendDobbelMessage()
    {
        Debug.Log("dobbelen");
        ArduinoDataManager.Instance.ButtonBPressed = false;
    }

    void GoBack()
    {
        ArduinoDataManager.Instance.ButtonBPressed = false;
        switch(sceneCount)
        {
            case 1:
                canvasIdle.SetActive(true);
                canvasMenu.SetActive(false);
                sceneCount--;
                break;
            case 2:
                menuText1.SetActive(true);
                menuText2.SetActive(false);
                sceneCount--;
                break;
            default:
                break;
        }
    }

    void MoveToIndexCount()
    {
        ArduinoDataManager.Instance.ButtonAPressed = false;
        switch (menuIndexCount)
        {
            case 0:
                if (sceneCount == 2)
                {
                    Debug.Log("Mission");
                }
                else
                {
                    Debug.Log("Event");
                }
                break;
            case 1:
                menuText1.SetActive(false);
                menuText2.SetActive(true);
                sceneCount++;
                menuIndexCount = 0;  // Reset index count when moving to the new menu
                selectedGameObject = arrayText2[0];
                SetTransparency(selectedGameObject, 1f);  // Set first item as selected
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    IEnumerator HandleMenuNavigation(GameObject[] menuArray)
    {
        isHandlingButtonPress = true;
        string joystickDirection = ArduinoDataManager.Instance.JoystickDirection;

        if (joystickDirection == "Up" && menuIndexCount != 0)
        {
            SetTransparency(menuArray[menuIndexCount], 100 / 255f);
            menuIndexCount--;
            selectedGameObject = menuArray[menuIndexCount];
            SetTransparency(menuArray[menuIndexCount], 1f);
            Debug.Log(selectedGameObject);
        }
        else if (joystickDirection == "Down" && menuIndexCount != menuArray.Length - 1)
        {
            SetTransparency(menuArray[menuIndexCount], 100 / 255f);
            menuIndexCount++;
            selectedGameObject = menuArray[menuIndexCount];
            SetTransparency(menuArray[menuIndexCount], 1f);
            Debug.Log(selectedGameObject);
        }

        yield return new WaitForSeconds(1f);
        ArduinoDataManager.Instance.ResetButtonStates();
        isHandlingButtonPress = false;
    }

    void SetTransparency(GameObject obj, float alpha)
    {
        TMP_Text textMesh = obj.GetComponent<TMP_Text>();
        if (textMesh != null)
        {
            Color color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
        }
    }

    void Start()
    {
        arrayText1 = GetChildren(menuText1);
        arrayText2 = GetChildren(menuText2);

        selectedGameObject = arrayText1[0];
        SetTransparency(selectedGameObject, 1f);  // Highlight the first menu item by default
    }

    void Update()
    {
        switch (sceneCount)
        {
            case 1:
                if (!string.IsNullOrEmpty(ArduinoDataManager.Instance.JoystickDirection) && !isHandlingButtonPress)
                {
                    StartCoroutine(HandleMenuNavigation(arrayText1));
                }
                else if (ArduinoDataManager.Instance.ButtonBPressed)
                {
                    GoBack();
                }
                else if (ArduinoDataManager.Instance.ButtonAPressed)
                {
                    MoveToIndexCount();
                }
                break;
            case 2:
                if (!string.IsNullOrEmpty(ArduinoDataManager.Instance.JoystickDirection) && !isHandlingButtonPress)
                {
                    StartCoroutine(HandleMenuNavigation(arrayText2));
                }
                else if (ArduinoDataManager.Instance.ButtonBPressed)
                {
                    GoBack();
                }
                break;
            case 3:
                break;
            default:
                if (ArduinoDataManager.Instance.ButtonAPressed)
                {
                    NavigateToMenu();
                }
                else if (ArduinoDataManager.Instance.ButtonBPressed)
                {
                    SendDobbelMessage();
                }
                break;
        }
    }
}
