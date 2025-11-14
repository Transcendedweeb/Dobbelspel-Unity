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
        // Use Random.Range(1, 101) to get 1-100 (100 possible values)
        // This allows for exact percentage splits (e.g., 60/40)
        int roll = Random.Range(1, 101);
        
        switch (level)
        {
            case 0: // Bronze (Level 1)
                if (roll <= 5) // gold - 5% chance (1-5)
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else if (roll <= 35) // silver - 30% chance (6-35)
                {
                    if (vfxSilver != null) vfxSilver.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 4eu verdient";
                }
                else // bronze - 65% chance (36-100)
                {
                    if (vfxBronze != null) vfxBronze.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 3eu verdient";
                }
                break;
            case 1: // Silver (Level 2)
                if (roll <= 60) // silver - 60% chance (1-60)
                {
                    if (vfxSilver != null) vfxSilver.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 4eu verdient";
                }
                else // gold - 40% chance (61-100)
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                break;
            default: // Gold (Level 3)
                if (roll <= 55) // gold - 55% chance (1-55)
                {
                    if (vfxGold != null) vfxGold.SetActive(true);
                    if (RewardText != null) RewardText.text = "Je hebt 6eu of een casino cadeau verdient";
                }
                else // epic - 45% chance (56-100)
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
                // storedLevel: 1=Bronze, 2=Silver, 3=Gold
                // levelToUse: 0=Bronze, 1=Silver, 2=Gold
                int levelToUse = storedLevel - 1;
                
                RollChance(levelToUse);
                Animator animator = this.gameObject.GetComponent<Animator>();
                if (animator != null) animator.SetTrigger("Walk");
            }
        }
   }
}
