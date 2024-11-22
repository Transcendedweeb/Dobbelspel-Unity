using System.Collections.Generic;
using UnityEngine;

public class CasinoData : MonoBehaviour
{
    CreateQuestions createQuestions;
    public enum Subject
    {
        Dieren,
        Voetbal,
        Muziek,
        Geschiedenis,
        Astronomie,
        Wetenschap
    }

    public enum Level
    {
        Level1,
        Level2,
        Level3
    }

    // Class to represent each question with answers
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public List<string> answers;  // List to hold 4 answers
        public int correctAnswerIndex; // Index of the correct answer in 'answers' list

        public Question(string questionText, List<string> answers, int correctAnswerIndex)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }

    // Dictionary to store questions by subject and level
    Dictionary<Subject, Dictionary<Level, List<Question>>> quizData;

    void Awake()
    {
        // Initialize quiz data
        quizData = new Dictionary<Subject, Dictionary<Level, List<Question>>>();
        createQuestions = this.gameObject.GetComponent<CreateQuestions>();

        // Initialize subjects and levels with empty question lists
        foreach (Subject subject in System.Enum.GetValues(typeof(Subject)))
        {
            quizData[subject] = new Dictionary<Level, List<Question>>();

            foreach (Level level in System.Enum.GetValues(typeof(Level)))
            {
                quizData[subject][level] = new List<Question>();
            }
        }

        createQuestions.Load();
        // LogQuizData();
    }

    // Method to add a question to the quiz data
    public void AddQuestion(Subject subject, Level level, string questionText, List<string> answers, int correctAnswerIndex)
    {
        // Ensure there are exactly 4 answers
        if (answers.Count != 4)
        {
            Debug.LogError("Each question must have exactly 4 possible answers.");
            return;
        }

        // Ensure correctAnswerIndex is valid
        if (correctAnswerIndex < 0 || correctAnswerIndex >= answers.Count)
        {
            Debug.LogError("Correct answer index is out of range.");
            return;
        }

        // Create and add the question
        Question question = new Question(questionText, answers, correctAnswerIndex);
        quizData[subject][level].Add(question);
    }

    // Method to retrieve questions for a given subject and level
    public List<Question> GetQuestions(Subject subject, Level level)
    {
        if (quizData.ContainsKey(subject) && quizData[subject].ContainsKey(level))
        {
            return quizData[subject][level];
        }
        Debug.LogError("No questions found for specified subject and level.");
        return null;
    }

    void LogQuizData()
    {
        Debug.Log("Printing the entire quizData structure:");
        foreach (var subjectPair in quizData)
        {
            Subject subject = subjectPair.Key;
            Debug.Log($"Subject: {subject}");
            
            foreach (var levelPair in subjectPair.Value)
            {
                Level level = levelPair.Key;
                Debug.Log($"  Level: {level}");
                
                foreach (var question in levelPair.Value)
                {
                    Debug.Log($"    Question: {question.questionText}");
                    for (int i = 0; i < question.answers.Count; i++)
                    {
                        string answerText = question.answers[i];
                        bool isCorrect = (i == question.correctAnswerIndex);
                        Debug.Log($"      Answer {i + 1}: {answerText} {(isCorrect ? "(Correct)" : "")}");
                    }
                }
            }
        }
    }
}
