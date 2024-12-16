using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShipToLocation : MonoBehaviour
{
    public GameObject mainParent;
    public Transform[] locations;
    public float moveSpeed = 5f;
    public float locationThreshold = 0.1f;
    public float resetTime = 1f;
    BossAI bossAI;
    Transform closestLocation;

    void OnEnable()
    {
        bossAI = mainParent.GetComponent<BossAI>();
        closestLocation = GetClosestLocation();
        Invoke("InvokeReset", resetTime);
    }

    void Update()
    {
        if (closestLocation != null)
        {
            Vector3 direction = (closestLocation.position - mainParent.transform.position).normalized;
            mainParent.transform.position += direction * moveSpeed * Time.deltaTime;
            mainParent.transform.LookAt(closestLocation);
            if (Vector3.Distance(mainParent.transform.position, closestLocation.position) <= locationThreshold)
            {
                End();
            }
        }
    }

    Transform GetClosestLocation()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform location in locations)
        {
            float distance = Vector3.Distance(mainParent.transform.position, location.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = location;
            }
        }

        return closest;
    }

    void InvokeReset()
    {
        bossAI.InvokeReset();
    }

    void End()
    {
        this.gameObject.SetActive(false);
    }
}
