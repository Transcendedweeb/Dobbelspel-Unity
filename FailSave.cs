using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailSave : MonoBehaviour
{
    void ClearPlayerPrefs()
    {
        Debug.LogWarning("Player prefs are cleared");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    void ReloadScene()
    {
        Debug.LogWarning("Reloading scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void IdleState()
    {
        Debug.LogWarning("Returning to idle state");
        SceneManager.LoadScene("Idle Scene");
    }

    void MainState()
    {
        Debug.LogWarning("Returning to main state");
        SceneManager.LoadScene("Main Scene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ClearPlayerPrefs();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            IdleState();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            MainState();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SerialReader serialReader = FindObjectOfType<SerialReader>();
            if (serialReader != null)
            {
                serialReader.Dobbel();
            }
        }
    }
}
