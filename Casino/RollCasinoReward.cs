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
   public TextMeshProUGUI CorrectAnswerText; // New text to show the correct answer

   bool end = false;
   bool defeat = false;
   bool con = false;
   bool rolled = false;
   int storedLevel = 1; // Store the level index

   public void CheckVictroy(int choice)
   {
        if (questionCanvas != null) questionCanvas.SetActive(false);
        
        WriteQuestionOnScreen wq = this.GetComponent<WriteQuestionOnScreen>();
        if (wq == null)
        {
            Debug.LogError("WriteQuestionOnScreen component not found.");
            return;
        }

        int correctIndex = wq.correctIndex;
        GetQuestion gq = this.GetComponent<GetQuestion>();
        
        // Display the correct answer
        if (CorrectAnswerText != null && gq != null && gq.answers != null && correctIndex >= 0 && correctIndex < gq.answers.Count)
        {
            string correctAnswer = gq.answers[correctIndex];
            CorrectAnswerText.text = $"Correct antwoord: {correctAnswer}";
            CorrectAnswerText.gameObject.SetActive(true);
        }

        if (choice == correctIndex)
        {
            con = true;
            if (completeIcon != null) completeIcon.SetActive(true);
        }
        else
        {
            defeat = true;
            if (defeatIcon != null) defeatIcon.SetActive(true);
        }
   }

   public void SetLevel(int level)
   {
        storedLevel = level;
   }

   public void RollChance(int level)
   {
        int roll = Random.Range(0, 101);
        switch (level)
        {
            case 0:
                if (roll <= 5) // gold
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else if (roll <= 35) // silver
                {
                    if (vfxSilver != null) vfxSilver.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 4eu verdient";
                }
                else // bronze
                {
                    if (vfxBronze != null) vfxBronze.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 3eu verdient";
                }
                break;
            case 1:
                if (roll <= 60) // silver
                {
                    if (vfxSilver != null) vfxSilver.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 4eu verdient";
                }
                else // gold
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                break;
            default:
                if (roll <= 55) // gold
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else // epic
                {
                    if (vfxPurple != null) vfxPurple.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 8eu of 4eu en een casino cadeau verdient";
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
                if (endingIcons != null) Destroy(endingIcons);
                
                // Use stored level (convert from 1-3 to 0-2 for RollChance)
                int levelToUse = storedLevel - 1; // Convert to 0-based index (1->0, 2->1, 3->2)
                CasinoUi casinoUi = this.GetComponent<CasinoUi>();
                if (casinoUi != null && casinoUi.enabled)
                {
                    levelToUse = casinoUi.currentIndex;
                }
                
                RollChance(levelToUse);
                Animator animator = this.gameObject.GetComponent<Animator>();
                if (animator != null) animator.SetTrigger("Walk");
            }
        }
   }
}
