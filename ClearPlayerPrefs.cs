using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    void OnEnable()
    {
        Debug.LogWarning("Player prefs are cleared");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
