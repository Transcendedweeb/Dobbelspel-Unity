using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketForward : MonoBehaviour
{
    public GameObject parent;
    public GameObject explosion;
    public GameObject[] effects;
    public float speed = 10f;

    bool exploded = false;

    void Update()
    {
        if (!exploded) transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        DisableEffects();
        explosion?.SetActive(true);
        exploded = true;
        Invoke("DestroyParent", 2f);
    }

    void DisableEffects()
    {
        foreach (GameObject effect in effects)
        {
            effect.SetActive(false);
        }
    }

    void DestroyParent()
    {
        Destroy(parent);
    }
}