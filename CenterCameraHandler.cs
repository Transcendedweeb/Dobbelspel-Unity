using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CenterCameraHandler : MonoBehaviour
{
    public GameObject canvasIdle;
    public GameObject canvasEvent;
    public GameObject canvasMenu;
    public GameObject menuText1;
    public GameObject menuText2;
    public GameObject menuText3;
    public GameObject dealer;
    public GameObject colloseum;
    public GameObject roulette;
    public GameObject casino;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    int sceneCount = 0;
    int menuIndexCount = 0;
    bool isHandlingButtonPress = false;
    int selectedMissionIndex = -1;
    GameObject[] arrayText1;
    GameObject[] arrayText2;
    GameObject[] arrayText3;
    GameObject selectedGameObject;
    ChangeCamera changeCamera;

    void ResetVars()
    {
        sceneCount = 0;
        menuIndexCount = 0;
        isHandlingButtonPress = false;
        selectedMissionIndex = -1;
        InitializeMenu(arrayText1);
        menuText1.SetActive(true);
        if (menuText2 != null) menuText2.SetActive(false);
        if (menuText3 != null) menuText3.SetActive(false);
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
        SerialReader serialReader = FindObjectOfType<SerialReader>();
        if (serialReader != null)
        {
            serialReader.Dobbel();
        }
        StartCoroutine(DobbelSound());
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
                if (menuText3 != null) menuText3.SetActive(false);
                sceneCount--;
                InitializeMenu(arrayText1);
                break;
            case 3:
                menuText2.SetActive(true);
                menuText3.SetActive(false);
                sceneCount--;
                InitializeMenu(arrayText2);
                selectedMissionIndex = -1;
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
        else if (sceneCount == 2)
        {
            // Store selected mission and show player count menu
            selectedMissionIndex = menuIndexCount;
            menuText2.SetActive(false);
            menuText3.SetActive(true);
            sceneCount++;
            InitializeMenu(arrayText3);
        }
        else if (sceneCount == 3)
        {
            // Load scene with selected player count
            int playerCount = menuIndexCount + 1; // 0-3 becomes 1-4
            LoadMissionWithPlayerCount(selectedMissionIndex, playerCount);
        }
    }

    void LoadMissionWithPlayerCount(int missionIndex, int playerCount)
    {
        // Store player count in a way that can be accessed by the loaded scene
        // You may want to use PlayerPrefs, a static class, or another method
        PlayerPrefs.SetInt("PlayerCount", playerCount);
        PlayerPrefs.Save();

        string sceneName = "";
        switch (missionIndex)
        {
            case 0:
                sceneName = "Mission 1 - space";
                break;
            case 1:
                sceneName = "Mission 2 - snow";
                break;
            case 2:
                sceneName = "Mission 3 - water";
                break;
            case 3:
                sceneName = "Mission 4 - desert";
                break;
        }

        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Loading {sceneName} with {playerCount} player(s)");
            SceneManager.LoadScene(sceneName);
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

        if ((joystickDirection == "Up" || joystickDirection == "RightUp" || joystickDirection == "LeftUp") && menuIndexCount != 0)
        {
            SetTransparency(menuArray[menuIndexCount], 100 / 255f);
            menuIndexCount--;
            selectedGameObject = menuArray[menuIndexCount];
            SetTransparency(menuArray[menuIndexCount], 1f);
            Debug.Log(selectedGameObject);
        }
        else if ((joystickDirection == "Down" || joystickDirection == "LeftDown" || joystickDirection == "RightDown")  && menuIndexCount != menuArray.Length - 1)
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
        if (menuText3 != null) arrayText3 = GetChildren(menuText3);

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
    IEnumerator DobbelSound()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            audioSource.clip = audioClips[i];
            audioSource.Play();

            yield return new WaitForSeconds(1.2f); 
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
                if (!string.IsNullOrEmpty(ArduinoDataManager.Instance.JoystickDirection) && !isHandlingButtonPress)
                {
                    StartCoroutine(HandleMenuNavigation(arrayText3));
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
                    changeCamera.ChangeToSpecificCam(casino);
                }
                break;
        }
    }
}
