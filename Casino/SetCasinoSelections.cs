using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCasinoSelections : MonoBehaviour
{
    public string[] subjects;
    public TextMeshProUGUI bronzeText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI goldText;

    int bronzeIndex;
    int silverIndex;
    int goldIndex;
    bool hasRolled = false; // Prevent double initialization

    void Start()
    {
        Roll(); // Roll() now has its own guard
    }

    void Roll()
    {
        // Prevent double execution
        if (hasRolled)
        {
            return;
        }

        if (subjects.Length == 0)
        {
            Debug.LogWarning("No subjects written down");
            return;
        }

        if (subjects.Length < 3)
        {
            Debug.LogWarning($"Not enough subjects! Need at least 3, but only have {subjects.Length}");
            return;
        }

        // Mark as rolled before proceeding
        hasRolled = true;

        List<int> rolledNumbers = GenerateUniqueRandomNumbers(0, subjects.Length-1, 3);

        bronzeIndex = rolledNumbers[0];
        silverIndex = rolledNumbers[1];
        goldIndex = rolledNumbers[2];

        SetText();
    }

    void SetText()
    {
        bronzeText.text = subjects[bronzeIndex];
        silverText.text = subjects[silverIndex];
        goldText.text = subjects[goldIndex];
    }

    string GetSubject(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                return subjects[bronzeIndex];
            case 1:
                return subjects[silverIndex];
            default:
                return subjects[goldIndex];
        }
    }

    List<int> GenerateUniqueRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();

        // Ensure we don't request more numbers than available
        int availableCount = max - min + 1;
        if (count > availableCount)
        {
            count = availableCount;
        }

        // Add all numbers from min to max (inclusive)
        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }

        // Fisher-Yates shuffle algorithm
        for (int i = 0; i < numbers.Count; i++)
        {
            int randomIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        // Return the first 'count' numbers
        return numbers.GetRange(0, count);
    }
}
