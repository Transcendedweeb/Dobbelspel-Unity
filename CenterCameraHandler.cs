using System.Collections;
using UnityEngine;
using TMPro;

public class CenterCameraHandler : MonoBehaviour
{
    public GameObject canvasIdle;
    public GameObject canvasEvent;
    public GameObject canvasMenu;
    public GameObject menuText1;
    public GameObject menuText2;
    public GameObject dealer;
    public GameObject colloseum;
    public GameObject roulette;
    int sceneCount = 0;
    int menuIndexCount = 0;
    bool isHandlingButtonPress = false;
    GameObject[] arrayText1;
    GameObject[] arrayText2;
    GameObject selectedGameObject;
    ChangeCamera changeCamera;

    void ResetVars()
    {
        sceneCount = 0;
        menuIndexCount = 0;
        isHandlingButtonPress = false;
        InitializeMenu(arrayText1);
        menuText1.SetActive(true);
        canvasIdle.SetActive(true);
        canvasMenu.SetActive(false);
        canvasEvent.SetActive(false);
    }

    GameObject[] GetChildren(GameObject parent)
    {
        if (parent == null) return new GameObject[0];
        
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
        InitializeMenu(arrayText1);
    }

    void SendDobbelMessage()
    {
        Debug.Log("dobbelen");
        ArduinoDataManager.Instance.ButtonBPressed = false;
    }

    void GoBack()
    {
        ArduinoDataManager.Instance.ButtonBPressed = false;
        switch (sceneCount)
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
                InitializeMenu(arrayText1);
                break;
            default:
                break;
        }
    }

    void MoveToIndexCount()
    {
        ArduinoDataManager.Instance.ButtonAPressed = false;
        if (sceneCount == 1)
        {
            switch (menuIndexCount)
            {
                case 0:
                    Debug.Log("Event");
                    menuText1.SetActive(false);
                    canvasEvent.SetActive(true);
                    this.enabled = false;
                    break;
                case 1:
                    Debug.Log("Mission menu");
                    menuText1.SetActive(false);
                    menuText2.SetActive(true);
                    sceneCount++;
                    InitializeMenu(arrayText2);
                    break;
                case 2:
                    Debug.Log("Dealer");
                    changeCamera.ChangeToSpecificCam(dealer);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (menuIndexCount)
            {
                case 0:
                    Debug.Log("TEST");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }

    void InitializeMenu(GameObject[] menuArray)
    {
        if (menuArray == null || menuArray.Length == 0) return;
        
        menuIndexCount = 0;
        for (int i = 0; i < menuArray.Length; i++)
        {
            if (menuArray[i] != null)
            {
                SetTransparency(menuArray[i], i == 0 ? 1f : 100 / 255f);
            }
        }
        selectedGameObject = menuArray.Length > 0 ? menuArray[0] : null;
    }

    IEnumerator HandleMenuNavigation(GameObject[] menuArray)
    {
        isHandlingButtonPress = true;
        string joystickDirection = ArduinoDataManager.Instance.JoystickDirection;

        if (menuArray == null || menuArray.Length == 0) yield break;

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
        if (obj == null) return;
        
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
        if (menuText1 != null) arrayText1 = GetChildren(menuText1);
        if (menuText2 != null) arrayText2 = GetChildren(menuText2);

        changeCamera = this.GetComponent<ChangeCamera>();
        InitializeMenu(arrayText1);
    }

    void OnEnable()
    {
        ResetVars();
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSrc in allAudioSources)
        {
            audioSrc.Stop();
        }
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
                else if (ArduinoDataManager.Instance.ButtonAPressed)
                {
                    MoveToIndexCount();
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
                else if (ArduinoDataManager.Instance.ButtonCPressed)
                {
                    ArduinoDataManager.Instance.ButtonCPressed = false;
                    Debug.Log("C - Arena");
                    changeCamera.ChangeToSpecificCam(colloseum);
                }
                else if (ArduinoDataManager.Instance.ButtonDPressed)
                {
                    ArduinoDataManager.Instance.ButtonDPressed = false;
                    Debug.Log("D - Roulette");
                    changeCamera.ChangeToSpecificCam(roulette);
                }
                else if (ArduinoDataManager.Instance.ButtonEPressed)
                {
                    ArduinoDataManager.Instance.ButtonEPressed = false;
                    Debug.Log("E - Casino");
                    changeCamera.ChangeToSpecificCam(roulette);
                }
                break;
        }
    }
}
