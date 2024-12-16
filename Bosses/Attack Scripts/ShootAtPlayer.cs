using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject muzzle;
    public GameObject playerMarker;
    public float waitTime = 1f;
    public float reactionTime = 1f;
    public float shotTime = 1f;
    public float projectileCount = 1f;
    public string animName = "";
    public bool quickReset = false;
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
        if (quickReset) bossAI.InvokeReset();
        playerMarker.SetActive(true);
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        if (animName != "") animator.SetBool(animName, true);
        yield return new WaitForSeconds(waitTime);

        changeEffectColor.effectColor = Color.red;
        changeEffectColor.ApplyColorToChildren();

        yield return new WaitForSeconds(reactionTime);

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPosition = muzzle.transform.position;
            Quaternion spawnRotation = Quaternion.LookRotation(playerMarker.transform.position - muzzle.transform.position);
            Instantiate(prefab, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(shotTime);
        }

        End();
    }

    void End()
    {
        if (animName != "") animator.SetBool(animName, false);
        if (!quickReset) bossAI.InvokeReset();
        playerMarker.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
