using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRouletteCanvasAnimation : MonoBehaviour
{
    public GameObject canvas3D;

    RouletteHandler rouletteHandler;

    void TriggerAnimation()
    {
        string button = rouletteHandler.nextButton;
        switch (button)
        {
            case "A":
                SetTriggerObject(rouletteHandler.clickA);
                break;
            case "B":
                SetTriggerObject(rouletteHandler.clickB);
                break;
            case "Up":
                SetTriggerObject2(rouletteHandler.clickUp);
                break;
            case "Down":
                SetTriggerObject2(rouletteHandler.clickDown);
                break;
            case "Left":
                SetTriggerObject2(rouletteHandler.clickLeft);
                break;
            default:
                SetTriggerObject2(rouletteHandler.clickRight);
                break;
        }
    }

    void SetTriggerObject(GameObject buttonGameObject)
    {
        buttonGameObject.GetComponent<Animator>().SetTrigger("Effect");
    }

    void SetTriggerObject2(GameObject buttonGameObject)
    {
        buttonGameObject.GetComponent<Animator>().SetTrigger("Effect 2");
    }

    void Awake()
    {
        rouletteHandler = canvas3D.GetComponent<RouletteHandler>();
    }
}
