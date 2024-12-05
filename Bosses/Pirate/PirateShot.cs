using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShot : MonoBehaviour
{
    public GameObject prefab;
    public GameObject muzzle;
    public GameObject playerMarker;
    public float waitTime = 1f;
    public float reactionTime = 1f;
    public float shotTime = 1f;
    public BossAI bossAI;
    public Animator animator;
    ChangeEffectColor changeEffectColor;

    void Start()
    {
        transform.position = muzzle.transform.position;
        transform.rotation = muzzle.transform.rotation;
    }

    void OnEnable()
    {
        changeEffectColor = playerMarker.GetComponent<ChangeEffectColor>();
        changeEffectColor.effectColor = Color.white;
        changeEffectColor.ApplyColorToChildren();
        playerMarker.SetActive(true);
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        animator.SetBool("Aiming", true);
        yield return new WaitForSeconds(waitTime);

        changeEffectColor.effectColor = Color.red;
        changeEffectColor.ApplyColorToChildren();

        yield return new WaitForSeconds(reactionTime);

        Vector3 spawnPosition = muzzle.transform.position;
        Quaternion spawnRotation = Quaternion.LookRotation(playerMarker.transform.position - muzzle.transform.position);
        Instantiate(prefab, spawnPosition, spawnRotation);

        yield return new WaitForSeconds(shotTime);

        animator.SetBool("Aiming", false);
        bossAI.InvokeReset();
        playerMarker.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
