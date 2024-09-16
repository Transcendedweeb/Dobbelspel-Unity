using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
