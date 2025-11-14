using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteQuestionOnScreen : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI aText;
    public TextMeshProUGUI bText;
    public TextMeshProUGUI cText;
    public TextMeshProUGUI dText;
    public GameObject canvasSubject;
    public GameObject canvasNext;
    public int correctIndex = 0;

    public void WriteText(string q, List<string> aList, int i)
    {
        if (aList == null || aList.Count < 4)
        {
            Debug.LogError("Invalid answers list provided.");
            return;
        }

        correctIndex = i;
        if (canvasNext != null) canvasNext.SetActive(true);
        if (canvasSubject != null) Destroy(canvasSubject);
        if (questionText != null) questionText.text = q;
        if (aText != null) aText.text = aList[0];
        if (bText != null) bText.text = aList[1];
        if (cText != null) cText.text = aList[2];
        if (dText != null) dText.text = aList[3];
    }
}
