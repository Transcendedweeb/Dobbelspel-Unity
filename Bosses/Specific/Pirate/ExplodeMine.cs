using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMine : MonoBehaviour
{
    public GameObject indicator;
    public GameObject explosion;

    public int damage = 50;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            healthManager.AdjustHealth(damage);

            indicator.SetActive(false);
            explosion.SetActive(true);

            Invoke("DestroySelf", 2f);
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
