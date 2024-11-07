using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouletteOutcome : MonoBehaviour
{
    public GameObject rouletteGameObject;
    public GameObject deathCanvas;
    public GameObject reaper;
    public bool defeat = false;

    void Update()
    {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ButtonBPressed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnEnable() 
    {
        ArduinoDataManager.Instance.ButtonBPressed = false;
        if (defeat) return;
    }
}
