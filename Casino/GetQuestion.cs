    using System.Collections.Generic;
    using UnityEngine;

    public class GetQuestion : MonoBehaviour
    {
        CasinoData casinoData;

        public string question;
        public List<string> answers;
        public int correctAnswerIndex;

        void Start()
        {
            casinoData = this.gameObject.GetComponent<CasinoData>();
        }

        public void Set(string subject, int level)
        {
            // Converteer de string-parameters naar enum-waarden
            CasinoData.Subject subjectEnum;
            CasinoData.Level levelEnum;

            if (!System.Enum.TryParse(subject, true, out subjectEnum))
            {
                Debug.LogError($"Subject '{subject}' is invalid.");
                return;
            }

            if (!System.Enum.TryParse($"Level{level}", true, out levelEnum))
            {
                Debug.LogError($"Level '{level}' is invalid.");
                return;
            }

            List<CasinoData.Question> questions = casinoData.GetQuestions(subjectEnum, levelEnum);

            if (questions == null || questions.Count == 0)
            {
                Debug.LogError($"No questions available for subject '{subject}' and level '{level}'.");
                return;
            }

            // Genereer een unieke sleutel voor deze combinatie van subject en level
            string key = $"{subject}Index{level}";

            // Haal de huidige index op (default 0 als deze nog niet bestaat)
            int levelIndex = PlayerPrefs.GetInt(key, 0);

            // Wrap around if index exceeds available questions
            levelIndex = levelIndex % questions.Count;

            // Werk de index bij en sla deze op
            PlayerPrefs.SetInt(key, levelIndex + 1);
            PlayerPrefs.Save(); // Zorg ervoor dat de data wordt geschreven

            // Selecteer de vraag
            CasinoData.Question selectedQuestion = questions[levelIndex];

            if (selectedQuestion == null || selectedQuestion.answers == null || selectedQuestion.answers.Count != 4)
            {
                Debug.LogError("Invalid question data.");
                return;
            }

            question = selectedQuestion.questionText;
            answers = selectedQuestion.answers;
            correctAnswerIndex = selectedQuestion.correctAnswerIndex;
        }
    }
