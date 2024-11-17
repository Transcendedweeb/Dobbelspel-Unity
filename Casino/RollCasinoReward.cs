using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollCasinoReward : MonoBehaviour
{
   public GameObject defeatIcon; 
   public GameObject completeIcon; 
   public GameObject questionCanvas;

   bool end = false;
   bool defeat = false;
   bool con = false;

   public void CheckVictroy(int choice)
   {
        questionCanvas.SetActive(false);
        int correctIndex = this.GetComponent<WriteQuestionOnScreen>().correctIndex;
        if (choice == correctIndex)
        {
            con = true;
            completeIcon.SetActive(true);
        }
        else
        {
            defeat = true;
            defeatIcon.SetActive(true);
        }
   }

   public void RollChance()
   {
        
   }

   public void SetEnd()
   {
        end = true;
   }

   void ResetScene()
   {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ButtonBPressed = false;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
   }

   void Update()
   {
        if (defeat || (end && con))
        {
            ResetScene();
        }
        else if (con)
        {
            if (ArduinoDataManager.Instance.ButtonBPressed)
            {
                ArduinoDataManager.Instance.ButtonBPressed = false;
                Debug.Log("HelloWorld");
            }
        }
   }
}
