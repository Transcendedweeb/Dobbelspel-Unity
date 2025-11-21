using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerTransitionAnimationHandler : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Image targetImage;
    [SerializeField] private Material dissolveMaterial;
    public GameObject text;

    [Header("Settings")]
    readonly string clipProperty = "_AdvancedDissolveCutoutStandardClip";
    [SerializeField] private float transitionInTime = 1f;
    [SerializeField] private float transitionOutTime = 1f;
    [SerializeField] private float waitToOutTransition = 1f;

    private Coroutine currentRoutine;

    void OnEnable()
    {
        if (targetImage != null && dissolveMaterial != null)
            targetImage.material = new Material(dissolveMaterial);
        AnimateIn();
        Invoke(nameof(AnimateOut), waitToOutTransition);
    }

    private void Reset()
    {
        targetImage = GetComponent<Image>();
        if (targetImage != null)
            dissolveMaterial = targetImage.material;
    }

    public void AnimateIn()
    {
        StartAnimation(1f, 0f, transitionInTime);
        Invoke(nameof(EnableText), transitionInTime-.5f);
    }

    public void AnimateOut()
    {
        DisableText();
        StartAnimation(0f, 1f, transitionOutTime);
    }

    void StartAnimation(float startValue, float endValue, float duration)
    {
        if (targetImage == null || targetImage.material == null)
        {
            Debug.LogError("PlayerTransitionAnimationHandler: No dissolve material assigned.");
            return;
        }

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(AnimateProperty(startValue, endValue, duration));
    }

    IEnumerator AnimateProperty(float startValue, float endValue, float duration)
    {
        float t = 0f;
        Material mat = targetImage.material;

        mat.SetFloat(clipProperty, startValue);

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = Mathf.Lerp(startValue, endValue, t / duration);
            mat.SetFloat(clipProperty, lerp);
            yield return null;
        }

        mat.SetFloat(clipProperty, endValue);
        currentRoutine = null;
    }

    public void DisableText()
    {
        text.SetActive(false);
    }

        public void EnableText()
    {
        text.SetActive(true);
    }
}
