using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffectColor : MonoBehaviour
{
    [Header("Set Effect Color")]
    public Color effectColor = Color.white;

    void Start()
    {
        ApplyColorToChildren();
    }

    void OnValidate()
    {
        ApplyColorToChildren();
    }

    public void ApplyColorToChildren()
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in childRenderers)
        {
            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null)
                {
                    if (materials[i].HasProperty("_Color"))
                        materials[i].SetColor("_Color", effectColor);
                    if (materials[i].HasProperty("_TintColor"))
                        materials[i].SetColor("_TintColor", effectColor);
                    if (materials[i].HasProperty("_RimColor"))
                        materials[i].SetColor("_RimColor", effectColor);
                }
            }
        }
    }
}
