using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RollCasinoReward : MonoBehaviour
{
   public GameObject defeatIcon; 
   public GameObject completeIcon; 
   public GameObject questionCanvas;
   public GameObject canvas3D;
   public GameObject endingIcons;
   public GameObject vfxBronze;
   public GameObject vfxSilver;
   public GameObject vfxGold;
   public GameObject vfxPurple;
   public TextMeshProUGUI RewardText;

   bool end = false;
   bool defeat = false;
   bool con = false;
   bool rolled = false;

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

   public void RollChance(int level)
   {
        int roll = Random.Range(0, 101);
        switch (level)
        {
            case 0:
                if (roll <= 5) // gold
                {
                    vfxGold.SetActive(true);
                    RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else if (roll <= 35) // silver
                {
                    vfxSilver.SetActive(true);
                    RewardText.text = "Je hebt 4eu verdient";
                }
                else // bronze
                {
                    vfxBronze.SetActive(true);
                    RewardText.text = "Je hebt 3eu verdient";
                }
                break;
            case 1:
                if (roll <= 60) // silver
                {
                    vfxSilver.SetActive(true);
                    RewardText.text = "Je hebt 4eu verdient";
                }
                else // gold
                {
                    vfxGold.SetActive(true);
                    RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                break;
            default:
                if (roll <= 55) // gold
                {
                    vfxGold.SetActive(true);
                    RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else // epic
                {
                    vfxPurple.SetActive(true);
                    RewardText.text = "Je hebt 8eu of 4eu en een casino cadeau verdient";
                }
                break;
        }
   }

   public void SetEnd()
   {
        end = true;
   }

   void ResetScene()
   {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ResetButtonStates();
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
   }

   void DestroyCanvas()
   {
        Destroy(canvas3D);
   }

   void Update()
   {
        if (defeat || (end && con))
        {
            ResetScene();
        }
        else if (con)
        {
            if (ArduinoDataManager.Instance.ButtonBPressed && !rolled)
            {
                rolled = true;
                ArduinoDataManager.Instance.ResetButtonStates();
                Destroy(endingIcons);
                RollChance(this.GetComponent<CasinoUi>().currentIndex);
                this.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            }
        }
   }
}
