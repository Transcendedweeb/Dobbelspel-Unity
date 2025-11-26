using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEnableOtherObjects : MonoBehaviour
{
    [System.Serializable]
    public class DelayedObject
    {
        public GameObject targetObject;
        public float delay;
    }

    public List<DelayedObject> objectsToEnable = new();

    private void OnEnable()
    {
        StartCoroutine(EnableObjectsRoutine());
    }

    IEnumerator EnableObjectsRoutine()
    {
        foreach (var item in objectsToEnable)
        {
            if (item == null || item.targetObject == null)
                continue;

            yield return new WaitForSeconds(item.delay);

            item.targetObject.SetActive(true);
        }
    }
}
