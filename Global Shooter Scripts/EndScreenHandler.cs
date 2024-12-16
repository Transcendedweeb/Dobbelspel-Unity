using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenHandler : MonoBehaviour
{
    public float delay = 3f;
    bool ready = false;

    void OnEnable()
    {
        Invoke("SetReady", delay);
    }

    void Update()
    {
        if (ready && ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            SceneManager.LoadScene("Main Scene");
        }
    }

    void SetReady()
    {
        ready = true;
    }
}
