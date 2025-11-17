using System.Collections;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [Header("Object References")]
    public GameObject prefab;
    public GameObject muzzle;
    public Animator animator;

    [Header("Timing Settings")]
    public float waitTime = 1f;
    public float reactionTime = 1f;
    public float shotTime = 1f;

    [Header("Shooting Settings")]
    public float projectileCount = 1f;

    [Header("Animation Settings")]
    public string animName = "";

    [Header("Behavior Options")]
    public bool quickReset = false;

    ChangeEffectColor changeEffectColor;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;

    void Start()
    {
        transform.position = muzzle.transform.position;
        transform.rotation = muzzle.transform.rotation;
    }

    void OnEnable()
    {
        bossAI = transform.root.gameObject.GetComponent<BossAI>();
        playerRefProvider = transform.root.gameObject.GetComponent<PlayerReferenceProvider>();

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
        {
            changeEffectColor = playerMarker.GetComponent<ChangeEffectColor>();
            if (changeEffectColor != null)
            {
                changeEffectColor.effectColor = Color.white;
                changeEffectColor.ApplyColorToChildren();
            }
            playerMarker.SetActive(true);
        }

        if (quickReset)
            bossAI.InvokeReset();

        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        if (animName != "")
            animator.SetBool(animName, true);

        yield return new WaitForSeconds(waitTime);

        if (changeEffectColor != null)
        {
            changeEffectColor.effectColor = Color.red;
            changeEffectColor.ApplyColorToChildren();
        }

        yield return new WaitForSeconds(reactionTime);

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPosition = muzzle.transform.position;
            Vector3 markerPos = playerRefProvider.GetPlayerMarkerPosition();
            Quaternion spawnRotation = Quaternion.LookRotation(markerPos - muzzle.transform.position);
            Instantiate(prefab, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(shotTime);
        }

        End();
    }

    void End()
    {
        if (animName != "")
            animator.SetBool(animName, false);

        if (!quickReset)
            bossAI.InvokeReset();

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null)
            playerMarker.SetActive(false);
        
        this.gameObject.SetActive(false);
    }
}
