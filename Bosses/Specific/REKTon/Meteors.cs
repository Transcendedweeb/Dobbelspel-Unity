using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    public GameObject mainParent;
    public GameObject player;
    public GameObject meteorPrefab;
    public Transform[] locations;
    public float initialWaitTime = 2f;
    public float cooldown = 1f;
    public string animTriggerName = "";
    BossAI bossAI;
    Animator animator;
    List<GameObject> spawnedMeteors;

    void OnEnable()
    {
        bossAI = mainParent.GetComponent<BossAI>();
        animator = mainParent.GetComponent<Animator>();
        spawnedMeteors = new List<GameObject>();
        if (animTriggerName != "")
            animator.SetTrigger(animTriggerName);

        CreateMeteors();
        Invoke("SetAttack", initialWaitTime);
    }

    void CreateMeteors()
    {
        foreach (Transform location in locations)
        {
            GameObject meteor = Instantiate(meteorPrefab, location.position, Quaternion.identity, mainParent.transform);
            spawnedMeteors.Add(meteor);
        }
    }

    void SetAttack()
    {
        bossAI.InvokeReset();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        foreach (GameObject meteor in spawnedMeteors)
        {
            yield return new WaitForSeconds(cooldown);
            MeteorShot meteorShot = meteor.GetComponent<MeteorShot>();
            meteorShot.player = player;
            meteorShot.enabled = true;
        }

        yield return new WaitForSeconds(cooldown);
        this.gameObject.SetActive(false);
    }
}
