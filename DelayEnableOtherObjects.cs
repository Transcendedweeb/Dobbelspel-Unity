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

    [Header("Enable Listed Objects")]
    public List<DelayedObject> objectsToEnable = new();

    [Header("Children Enable Mode")]
    public bool useChildrenInstead = false;
    public bool reverseChildrenOrder = false;
    public float startDelay = 0f;
    public float childDelay = 0.2f;

    private void OnEnable()
    {
        StartCoroutine(EnableObjectsRoutine());
    }

    IEnumerator EnableObjectsRoutine()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        if (useChildrenInstead)
        {
            int childCount = transform.childCount;

            if (!reverseChildrenOrder)
            {
                for (int i = 0; i < childCount; i++)
                {
                    var child = transform.GetChild(i).gameObject;

                    if (child != null)
                        child.SetActive(true);

                    if (childDelay > 0f)
                        yield return new WaitForSeconds(childDelay);
                }
            }
            else
            {
                for (int i = childCount - 1; i >= 0; i--)
                {
                    var child = transform.GetChild(i).gameObject;

                    if (child != null)
                        child.SetActive(true);

                    if (childDelay > 0f)
                        yield return new WaitForSeconds(childDelay);
                }
            }

            yield break;
        }

        foreach (var item in objectsToEnable)
        {
            if (item == null || item.targetObject == null)
                continue;

            yield return new WaitForSeconds(item.delay);

            item.targetObject.SetActive(true);
        }
    }
}
