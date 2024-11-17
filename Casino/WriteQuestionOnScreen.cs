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
        correctIndex = i;
        canvasNext.SetActive(true);
        Destroy(canvasSubject);
        questionText.text = q;
        aText.text = aList[0];
        bText.text = aList[1];
        cText.text = aList[2];
        dText.text = aList[3];
    }
}
