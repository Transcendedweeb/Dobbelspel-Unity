using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffectColor : MonoBehaviour
{
    [Header("Set Effect Color")]
    public Color effectColor = Color.white;

    void Awake()
    {
        ApplyColorToChildren();
    }

    void OnValidate()
    {
        ApplyColorToChildren();
    }

    void ApplyColorToChildren()
    {
        // Get all Renderer components in the child objects (and the parent if it has one)
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in childRenderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Check if the material has the desired color properties before setting them
                if (material.HasProperty("_Color"))
                    material.SetColor("_Color", effectColor);
                if (material.HasProperty("_TintColor"))
                    material.SetColor("_TintColor", effectColor);
                if (material.HasProperty("_RimColor"))
                    material.SetColor("_RimColor", effectColor);
            }
        }
    }
}
