using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRandomPoint : MonoBehaviour
{
    [Header("References")]
    public GameObject boss;
    private Transform bossRoot;
    private BossAI bossAI;

    [Header("Locations")]
    public Transform[] locations;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float pointArrivalDistance = 0.1f;

    [Header("Rotation")]
    public float rotationSpeed = 7f;
    public Vector3 rotationOffset;

    [Header("Behavior")]
    public bool quickReset = false;

    // Internal
    private Transform chosenLocation;
    private bool running = false;

    void OnEnable()
    {
        bossRoot = boss.transform;
        bossAI = boss.GetComponent<BossAI>();

        if (quickReset)
            bossAI.InvokeReset();

        chosenLocation = GetRandomLocation();

        if (chosenLocation == null)
        {
            Debug.LogWarning("MoveShipToLocation: No valid locations assigned!");
            End();
            return;
        }

        running = true;
    }

    void Update()
    {
        if (!running || chosenLocation == null)
            return;

        Vector3 direction = chosenLocation.position - bossRoot.position;

        // Move
        bossRoot.position += direction.normalized * moveSpeed * Time.deltaTime;

        // Rotate (with offset)
        if (direction != Vector3.zero)
        {
            Quaternion lookRot =
                Quaternion.LookRotation(direction.normalized) *
                Quaternion.Euler(rotationOffset);

            bossRoot.rotation = Quaternion.Lerp(
                bossRoot.rotation,
                lookRot,
                rotationSpeed * Time.deltaTime
            );
        }

        // Arrived
        if (direction.magnitude <= pointArrivalDistance)
        {
            End();
        }
    }

    Transform GetRandomLocation()
    {
        if (locations == null || locations.Length == 0)
            return null;

        return locations[Random.Range(0, locations.Length)];
    }

    void End()
    {
        if (!quickReset) bossAI.InvokeReset();
        running = false;
        gameObject.SetActive(false);
    }
}
