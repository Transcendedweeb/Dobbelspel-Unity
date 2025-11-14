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

    void StartCasino()
    {
        Debug.LogWarning("Starting casino");
        CenterCameraHandler centerCameraHandler = FindObjectOfType<CenterCameraHandler>();
        if (centerCameraHandler != null && centerCameraHandler.casino != null)
        {
            ChangeCamera changeCamera = centerCameraHandler.GetComponent<ChangeCamera>();
            if (changeCamera != null)
            {
                changeCamera.ChangeToSpecificCam(centerCameraHandler.casino);
            }
        }
    }

    void StartColloseum()
    {
        Debug.LogWarning("Starting colloseum/penalty shootout");
        CenterCameraHandler centerCameraHandler = FindObjectOfType<CenterCameraHandler>();
        if (centerCameraHandler != null && centerCameraHandler.colloseum != null)
        {
            ChangeCamera changeCamera = centerCameraHandler.GetComponent<ChangeCamera>();
            if (changeCamera != null)
            {
                changeCamera.ChangeToSpecificCam(centerCameraHandler.colloseum);
            }
        }
    }

    void StartRoulette()
    {
        Debug.LogWarning("Starting roulette");
        CenterCameraHandler centerCameraHandler = FindObjectOfType<CenterCameraHandler>();
        if (centerCameraHandler != null && centerCameraHandler.roulette != null)
        {
            ChangeCamera changeCamera = centerCameraHandler.GetComponent<ChangeCamera>();
            if (changeCamera != null)
            {
                changeCamera.ChangeToSpecificCam(centerCameraHandler.roulette);
            }
        }
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
        else if (Input.GetKeyDown(KeyCode.K))
        {
            StartCasino();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            StartColloseum();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            StartRoulette();
        }
    }
}
