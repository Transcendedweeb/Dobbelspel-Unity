using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFireProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawn;
    public Transform target;
    public float downTime = 5f;
    public Vector3 rotationOffset;

    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        while (true)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(downTime);
        }
    }

    void SpawnProjectile()
    {
        Vector3 directionToTarget = target.position - spawn.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(rotationOffset);
        Instantiate(projectile, spawn.position, rotationToTarget);
    }
}
