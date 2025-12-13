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
    bool hasRolled = false;

    void Start()
    {
        Roll();
    }

    void Roll()
    {
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

        int availableCount = max - min + 1;
        if (count > availableCount)
        {
            count = availableCount;
        }

        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            int randomIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        return numbers.GetRange(0, count);
    }
}
