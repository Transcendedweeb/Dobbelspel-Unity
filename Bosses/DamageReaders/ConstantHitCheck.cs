using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantHitCheck : MonoBehaviour
{
    public int damage;
    public float checkTimeInSec = 1f;
    private HashSet<HealthManager> playersInside = new HashSet<HealthManager>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            if (!playersInside.Contains(healthManager))
            {
                playersInside.Add(healthManager);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
            if (playersInside.Contains(healthManager))
            {
                playersInside.Remove(healthManager);
            }
        }
    }

    void Start()
    {
        StartCoroutine(CheckForPlayersInside());
    }

    IEnumerator CheckForPlayersInside()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkTimeInSec);

            foreach (HealthManager healthManager in playersInside)
            {
                if (healthManager != null)
                {
                    if (healthManager.enabled) healthManager.AdjustHealth(damage);
                }
            }
        }
    }
}
