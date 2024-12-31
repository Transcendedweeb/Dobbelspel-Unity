using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailerHandler : MonoBehaviour
{
    public float minutes;
    public float seconds;

    void Start()
    {
        Invoke("NextScene", (minutes*60) + seconds);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
