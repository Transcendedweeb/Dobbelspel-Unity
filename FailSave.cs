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
    }
}
