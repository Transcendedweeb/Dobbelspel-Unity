using System.Collections;
using UnityEngine;

public class PlayerMarkerController : MonoBehaviour
{
    [Header("Timings")]
    public float enableTime = 0f;
    public float redTime = 1f;
    public float disableTime = 2f;

    PlayerReferenceProvider playerRefProvider;
    GameObject playerMarker;

    void OnEnable()
    {
        playerRefProvider = transform.root.GetComponent<PlayerReferenceProvider>();
        if (playerRefProvider == null)
        {
            Debug.LogWarning("PlayerMarkerController: No PlayerReferenceProvider found in parent.");
            return;
        }

        playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker == null)
        {
            Debug.LogWarning("PlayerMarkerController: No player marker found.");
            return;
        }

        StartCoroutine(MarkerRoutine());
    }

    IEnumerator MarkerRoutine()
    {
        if (enableTime > 0f)
            yield return new WaitForSeconds(enableTime);

        playerMarker.SetActive(true);

        var colorChanger = playerMarker.GetComponent<ChangeEffectColor>();
        if (colorChanger != null)
        {
            colorChanger.effectColor = Color.white;
            colorChanger.ApplyColorToChildren();
        }

        if (redTime > 0f)
            yield return new WaitForSeconds(redTime);

        if (colorChanger != null)
        {
            colorChanger.effectColor = Color.red;
            colorChanger.ApplyColorToChildren();
        }

        if (disableTime > 0f)
            yield return new WaitForSeconds(disableTime);

        playerMarker.SetActive(false);
    }
}
