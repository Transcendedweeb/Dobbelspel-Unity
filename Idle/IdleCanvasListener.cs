using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleCanvasListener : MonoBehaviour
{
    public GameObject console;
    public GameObject video;
    public float waitTimeToTrailer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (console.activeSelf) console.SetActive(false);
            else console.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MoveToTrailer());
        }
    }

    IEnumerator MoveToTrailer()
    {
        video.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitTimeToTrailer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
