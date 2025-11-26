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
    public float cooldown = 0f;

    [Header("Shooting Settings")]
    public int projectileCount = 1;

    [Header("Animation Settings")]
    public string animName = "";

    [Header("Behavior Options")]
    public bool quickReset = false;

    [Header("Sniper Line settings")]
    public bool setMarker = true;
    public bool muzzleLockOn = false;
    public bool setMuzzleChild = false;

    ChangeEffectColor changeEffectColor;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;

    private bool isRunning = false;


    void Start()
    {
        transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
    }


    void OnEnable()
    {
        if (isRunning)
            return;

        isRunning = true;

        if (muzzleLockOn)
            muzzle.GetComponent<LockOn>().enabled = true;

        bossAI = transform.root.GetComponent<BossAI>();
        playerRefProvider = transform.root.GetComponent<PlayerReferenceProvider>();

        GameObject playerMarker = playerRefProvider.GetPlayerMarker();
        if (playerMarker != null && setMarker)
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

        // Fire projectiles
        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPosition = muzzle.transform.position;
            Vector3 markerPos = playerRefProvider.GetPlayerMarkerPosition();

            Quaternion lookRotation = Quaternion.LookRotation(markerPos - muzzle.transform.position);

            Quaternion prefabBaseRotation = prefab.transform.rotation;
            Quaternion finalRotation = lookRotation * prefabBaseRotation;

            GameObject obj;

            if (setMuzzleChild)
            {
                obj = Instantiate(prefab, muzzle.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = prefabBaseRotation;
            }
            else
            {
                obj = Instantiate(prefab, spawnPosition, finalRotation);
            }

            yield return new WaitForSeconds(shotTime);
        }

        Invoke(nameof(End), cooldown);
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

        isRunning = false;
        gameObject.SetActive(false);
    }
}
