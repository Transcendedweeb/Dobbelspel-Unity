using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetBossName : MonoBehaviour
{
    public TMP_Text bossText;

    void Start()
    {
        if (bossText != null)
        {
            bossText.text = gameObject.name;
        }
    }
}
