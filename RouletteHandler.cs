using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteHandler : MonoBehaviour
{
    public GameObject canvas2D;
    public GameObject canvas3D;
    public GameObject canvasEffect;
    public GameObject clickA;
    public GameObject clickB;
    public GameObject clickUp;
    public GameObject clickDown;
    public GameObject clickLeft;
    public GameObject clickRight;
    public GameObject victory;
    public GameObject startText;
    public GameObject bossMan;
    [HideInInspector] public string nextButton;
    GameObject[] buttonArray = new GameObject[6];
    GameObject lastButtonChoice;
    bool endMinigame = false;
    bool SetNewButton = false;
    bool miniGameStarted = false;
    bool joystickPressed = false;
    bool joystickReleased = true;
    int correctClicks = 0;

    void Start()
    {
        buttonArray[0] = clickA;
        buttonArray[1] = clickB;
        buttonArray[2] = clickUp;
        buttonArray[3] = clickDown;
        buttonArray[4] = clickLeft;
        buttonArray[5] = clickRight;
    }

    public void TriggerStart()
    {
        Debug.Log("Minigame Start");
        SetNextButton();
        canvas3D.SetActive(true);
        StartCoroutine(PlayRoulette());
    }

    public void TriggerEnd()
    {
        Debug.Log("Minigame End");
        endMinigame = true;
    }

    void SetNextButton()
    {
        int randomIndex = Random.Range(0, buttonArray.Length);
        lastButtonChoice = buttonArray[randomIndex];
        switch (randomIndex)
        {
            case 0:
                nextButton = "A";
                break;
            case 1:
                nextButton = "B";
                break;
            case 2:
                nextButton = "Up";
                break;
            case 3:
                nextButton = "Down";
                break;
            case 4:
                nextButton = "Left";
                break;
            default:
                nextButton = "Right";
                break;
        }
        lastButtonChoice.SetActive(true);
    }

    void StopMiniGame()
    {
        endMinigame = false;
        SetNewButton = false;
        miniGameStarted = false;
        lastButtonChoice.SetActive(false);
        canvas3D.SetActive(false);
        canvas2D.SetActive(true);
    }

    void ListenButtonPress()
    {
        if (ArduinoDataManager.Instance.ButtonAPressed)
        {
            if (nextButton == "A") { CompleteButtonPress(); }
        }
        else if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            if (nextButton == "B") { CompleteButtonPress(); }
        }

        string joystickDirection = ArduinoDataManager.Instance.JoystickDirection;

        if (!string.IsNullOrEmpty(joystickDirection) && joystickReleased)
        {
            if (joystickDirection == nextButton)
            {
                CompleteButtonPress();
                joystickPressed = true;
                joystickReleased = false;
            }
        }

        if (string.IsNullOrEmpty(joystickDirection) && joystickPressed)
        {
            joystickPressed = false;
            joystickReleased = true;
        }

        ArduinoDataManager.Instance.ResetButtonStates();
    }

    void CompleteButtonPress()
    {
        correctClicks = correctClicks+1;
        canvasEffect.SetActive(true);
        SetNewButton = true;
    }

    void CalculateOutcome()
    {
        int survivalChance = 65 + correctClicks;
        correctClicks = 0;
        int roll = Random.Range(1, 101);
        Debug.LogWarning("survivalChance: " + survivalChance + " roll: " + roll);
        if (roll > survivalChance)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Defeat");
        }
        else 
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Victory");
            bossMan.GetComponent<Animator>().SetTrigger("Victory");
            victory.SetActive(true);
        }
    }

    IEnumerator PlayRoulette()
    {
        while (!endMinigame)
        {
            ListenButtonPress();
            if (SetNewButton)
            {
                SetNewButton = false;
                yield return new WaitForSeconds(.5f);
                SetNextButton();
            }
            else yield return null;
        }
        StopMiniGame();
        CalculateOutcome();
        this.enabled = false;
    }

    void Update()
    {
        if (ArduinoDataManager.Instance.ButtonAPressed && !miniGameStarted)
        {
            ArduinoDataManager.Instance.ButtonAPressed = false;
            Destroy(startText);
            miniGameStarted = true;
            canvas2D.SetActive(false);
            this.gameObject.GetComponent<Animator>().SetTrigger("Start");
        }
    }
}
