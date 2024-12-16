using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NepalmComponent : MonoBehaviour
{
    public int singleDamage = 75;
    public float explosionTime = 1f;
    public float timeConvert = .5f;
    SingleHit singleHit;
    ConstantHitCheck  constantHitCheck;


    void OnEnable()
    {
        constantHitCheck = GetComponent<ConstantHitCheck>();
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        yield return new WaitForSeconds(explosionTime);
        singleHit = this.gameObject.AddComponent<SingleHit>();
        singleHit.damage = singleDamage;
        yield return new WaitForSeconds(timeConvert);
        Destroy(singleHit);
        constantHitCheck.enabled = true;
    }
}
