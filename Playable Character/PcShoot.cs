using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcShoot : MonoBehaviour
{
    public GameObject muzzlePos; // De positie waar het projectiel wordt gespawned
    public GameObject shotPrefab; // Het prefab van het projectiel
    public LockOn lockOnScript; // Verwijzing naar het LockOn-script
    public float burstWaitTime; // Tussenpozen tussen schoten in een burst
    public float nextShotWaitTime; // Tussenpozen tussen bursts
    public Vector3 projectileRotationOffset; // Optionele offset voor de projectielrotatie
    public bool triggerRelease = false;
    private bool waiting = false;

    void Update()
    {
        if (ArduinoDataManager.Instance.ButtonAPressed)
        {
            ArduinoDataManager.Instance.ButtonAPressed = false;
            if (!waiting) 
            {
                waiting = true;
                triggerRelease = true;
                StartCoroutine(BurstFire());
            }
        }
    }

    void OnEnable()
    {
        waiting = false;
        triggerRelease = false;
    }

    IEnumerator BurstFire()
    {
        InstantiateShot();
        yield return new WaitForSeconds(burstWaitTime);

        InstantiateShot();
        yield return new WaitForSeconds(burstWaitTime);

        InstantiateShot();
        triggerRelease = false;
        yield return new WaitForSeconds(nextShotWaitTime);

        waiting = false;
    }

    void InstantiateShot()
    {
        // Controleer of het LockOn-script en het doel beschikbaar zijn
        if (lockOnScript != null && lockOnScript.target != null)
        {
            // Bereken de richting naar het doel
            Vector3 directionToTarget = lockOnScript.target.transform.position - muzzlePos.transform.position;
            directionToTarget.Normalize(); // Zorg ervoor dat de richting een eenheidsvector is

            // Bereken de rotatie die naar het doel wijst
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Pas de offset toe
            targetRotation *= Quaternion.Euler(projectileRotationOffset);

            // Spawn het projectiel met de berekende rotatie
            Instantiate(shotPrefab, muzzlePos.transform.position, targetRotation);
        }
        else
        {
            // Als er geen doel is, gebruik de standaard rotatie van het wapen
            Quaternion defaultRotation = muzzlePos.transform.rotation * Quaternion.Euler(projectileRotationOffset);
            Instantiate(shotPrefab, muzzlePos.transform.position, defaultRotation);
        }
    }
}
