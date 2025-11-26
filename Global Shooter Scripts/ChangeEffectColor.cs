using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffectColor : MonoBehaviour
{
    [Header("Color Settings")]
    public Color initialColor = Color.white;
    public Color effectColor = Color.white;
    public bool changeBaseMap = false;

    [Header("Set possible delay")]
    public float delay = 0f;

    void Start()
    {
        // Apply initial color instantly when spawned
        ApplyColorToChildren(initialColor);

        // If delay is set, change to final color later
        if (delay > 0f)
            StartCoroutine(DelayedColorChange());
        else
            ApplyColorToChildren(effectColor);
    }

    void OnValidate()
    {
        ApplyColorToChildren(effectColor);
    }

    IEnumerator DelayedColorChange()
    {
        yield return new WaitForSeconds(delay);
        ApplyColorToChildren(effectColor);
    }

    public void ApplyColorToChildren(Color color)
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
                        materials[i].SetColor("_Color", color);
                    if (materials[i].HasProperty("_TintColor"))
                        materials[i].SetColor("_TintColor", color);
                    if (materials[i].HasProperty("_RimColor"))
                        materials[i].SetColor("_RimColor", color);
                    if (changeBaseMap && materials[i].HasProperty("_BaseMap"))
                        materials[i].SetColor("_BaseColor", color);
                }
            }
        }
    }

    public void ApplyColorToChildren()
    {
        ApplyColorToChildren(effectColor);
    }
}