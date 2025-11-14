using System.Collections;
using UnityEngine;
using TMPro;

public class CasinoUi : MonoBehaviour
{
    SetCasinoSelections setCasinoSelections;
    GetQuestion getQuestion;
    WriteQuestionOnScreen wq;
    public GameObject[] menuOptions;
    public int currentIndex = 0;
    public int selectedLevel = 1; // Store the selected level for RollCasinoReward
    bool isHandlingInput = false;
    bool infoMenu = false;
    public float inputCooldown = 0.5f;
    public GameObject selectionUi;
    public GameObject infoUi;

    void Start()
    {
        setCasinoSelections = this.GetComponent<SetCasinoSelections>();
        getQuestion = this.GetComponent<GetQuestion>();
        wq = this.GetComponent<WriteQuestionOnScreen>();
        InitializeMenu();
    }

    void InitializeMenu()
    {
        if (menuOptions.Length == 0) return;

        for (int i = 0; i < menuOptions.Length; i++)
        {
            SetTransparency(menuOptions[i], i == currentIndex ? 1f : 0.4f);
        }
    }

    void Update()
    {
        string joystickDirection = ArduinoDataManager.Instance.JoystickDirection;

        if (!string.IsNullOrEmpty(joystickDirection))
        {
            if (joystickDirection == "Up" && !isHandlingInput)
            {
                isHandlingInput = true;
                Invoke("DisableCooldown", inputCooldown);
                NavigateMenu(-1);
            }
            else if (joystickDirection == "Down" && !isHandlingInput)
            {
                isHandlingInput = true;
                Invoke("DisableCooldown", inputCooldown);
                NavigateMenu(1);
            }
            ResetJoystick();
        }

        if (ArduinoDataManager.Instance.ButtonAPressed && !infoMenu)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            SelectOption();
        }
        else if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            if (!infoMenu)
            {
                infoMenu = true;
                selectionUi.SetActive(false);
                infoUi.SetActive(true);
            }
            else
            {
                infoMenu = false;
                selectionUi.SetActive(true);
                infoUi.SetActive(false);
            }
        }
    }

    void NavigateMenu(int direction)
    {
        SetTransparency(menuOptions[currentIndex], 0.4f);
        currentIndex = (currentIndex + direction + menuOptions.Length) % menuOptions.Length;
        SetTransparency(menuOptions[currentIndex], 1f); 
    }

    void SelectOption()
    {
        switch (currentIndex)
        {
            case 0:
                selectedLevel = 1;
                getQuestion.Set(setCasinoSelections.bronzeText.text, 1);
                break;
            case 1:
                selectedLevel = 2;
                getQuestion.Set(setCasinoSelections.silverText.text, 2);
                break;
            default:
                selectedLevel = 3;
                getQuestion.Set(setCasinoSelections.goldText.text, 3);
                break;
        }
        wq.WriteText(getQuestion.question, getQuestion.answers, getQuestion.correctAnswerIndex);
        
        // Store the level in RollCasinoReward
        RollCasinoReward reward = this.GetComponent<RollCasinoReward>();
        if (reward != null)
        {
            reward.SetLevel(selectedLevel);
        }
        
        this.enabled = false;
    }

    void SetTransparency(GameObject obj, float alpha)
    {
        if (obj == null) return;

        TMP_Text text = obj.GetComponent<TMP_Text>();
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    void DisableCooldown()
    {
        isHandlingInput = false;
    }

    void ResetJoystick()
    {
        ArduinoDataManager.Instance.JoystickButtonPressed = false;
        ArduinoDataManager.Instance.JoystickDirection = null;
    }
}