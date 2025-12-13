using UnityEngine;

public class SetLocalPositionOnSpawn : MonoBehaviour
{
    [Header("Spawn Local Position")]
    public Vector3 localPosition;

    void Awake()
    {
        Apply();
    }

    void OnEnable()
    {
        Apply();
    }

    void Apply()
    {
        transform.localPosition = localPosition;
    }

#if UNITY_EDITOR
    void Reset()
    {
        localPosition = transform.localPosition;
    }
#endif
}
