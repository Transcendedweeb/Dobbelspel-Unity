using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcShoot : MonoBehaviour
{
    public GameObject muzzlePos;
    public GameObject shotPrefab;
    public LockOn lockOnScript;
    public int shots = 1;
    public float burstWaitTime = 0;
    public float nextShotWaitTime = 1;
    public int bulletsPerShot = 1;
    public Vector3[] bulletsPerShotOffset;
    public Vector3 projectileRotationOffset;
    public AudioClip shootClip;
    [HideInInspector] public bool triggerRelease = false;
    
    AudioSource audioSource;
    
    bool waiting = false;

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
    
    void Start()
    {
        audioSource = muzzlePos.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        waiting = false;
        triggerRelease = false;
    }

    IEnumerator BurstFire()
    {

        for (int i = 0; i < shots; i++)
        {
            InstantiateShot();
            if (audioSource != null && shootClip != null) audioSource.PlayOneShot(shootClip);

            if (i == shots-1) triggerRelease = false;
            yield return new WaitForSeconds(burstWaitTime);
        }

        waiting = false;
    }

    void InstantiateShot()
    {
        Quaternion baseRotation;
        Vector3 basePosition = muzzlePos.transform.position;

        if (lockOnScript != null && lockOnScript.target != null)
        {
            Vector3 directionToTarget = lockOnScript.target.transform.position - basePosition;
            directionToTarget.Normalize();

            baseRotation = Quaternion.LookRotation(directionToTarget);
        }
        else
        {
            baseRotation = muzzlePos.transform.rotation;
        }

        baseRotation *= Quaternion.Euler(projectileRotationOffset);

        for (int b = 0; b < bulletsPerShot; b++)
        {
            Vector3 offset = Vector3.zero;
            if (bulletsPerShotOffset != null && b < bulletsPerShotOffset.Length)
                offset = bulletsPerShotOffset[b];

            Vector3 spawnPos = basePosition + muzzlePos.transform.TransformDirection(offset);

            Instantiate(shotPrefab, spawnPos, baseRotation);
        }
    }
}
