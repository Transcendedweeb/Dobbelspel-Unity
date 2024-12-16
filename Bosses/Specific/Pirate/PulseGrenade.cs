using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGrenade : MonoBehaviour
{
    public int damage = 50;
    public GameObject parent;
    public float destroyTime = 10f;
    public float waitTime = .25f;
    public float initialWaitTime = .1f;
    public int count = 6;

    AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        Invoke("DestroyParent", destroyTime);
        StartCoroutine(Main());
    }

    IEnumerator Main()
    {
        SphereCollider sphereCollider = this.gameObject.GetComponent<SphereCollider>();

        yield return new WaitForSeconds(initialWaitTime);
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(waitTime);
            audioSource.Play();

            sphereCollider.enabled = true;
            yield return new WaitForSeconds(waitTime);
            sphereCollider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            if (healthManager.enabled) healthManager.AdjustHealth(damage);
        }
    }

    void DestroyParent()
    {
        Destroy(parent);
    }
}
