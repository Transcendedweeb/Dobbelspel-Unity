using System.Collections.Generic;
using UnityEngine;

public class GetQuestion : MonoBehaviour
{
    CasinoData casinoData;

    public string question;
    public List<string> answers;
    public int correctAnswerIndex;

    CasinoData.Subject currentSubject;

    void Start()
    {
        casinoData = this.gameObject.GetComponent<CasinoData>();
    }

    public void Set(string subject, int level)
    {
        // Converteer de string-parameters naar enum-waarden
        CasinoData.Subject subjectEnum;
        CasinoData.Level levelEnum;

        // Haal de vragen op voor het opgegeven onderwerp en niveau
        List<CasinoData.Question> questions = casinoData.GetQuestions(subjectEnum, levelEnum);

        // Kies een willekeurige vraag
        CasinoData.Question selectedQuestion = questions[Random.Range(0, questions.Count)];

        // Vul de variabelen
        question = selectedQuestion.questionText;
        answers = selectedQuestion.answers;
        correctAnswerIndex = selectedQuestion.correctAnswerIndex;

        Debug.Log($"Question set: {question} (Correct Answer Index: {correctAnswerIndex})");
    }
}
