using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleCanvasListener : MonoBehaviour
{
    public GameObject console;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (console.activeSelf) console.SetActive(false);
            else console.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
