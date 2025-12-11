using System.Collections;
using UnityEngine;

public class WindupDashAttack : MonoBehaviour
{
    [Header("Prefabs (Optional)")]
    public GameObject windupPrefab;
    public bool windupAsChild = false;
    public Vector3 windupOffset = Vector3.zero;

    public GameObject dashPrefab;
    public bool dashAsChild = false;
    public Vector3 dashOffset = Vector3.zero;

    public GameObject endPrefab;
    public bool endAsChild = false;
    public Vector3 endOffset = Vector3.zero;

    [Header("Windup")]
    public float windupTime = 1f;

    [Header("Dash")]
    public float dashSpeed = 7f;
    public float stopDistance = 0.2f;
    public Vector3 dashPositionOffset = Vector3.zero;

    [Header("Height Options")]
    public bool useFixedY = false;
    public float fixedYPosition = 0f;

    [Header("Animation")]
    public string animationName = "State";

    [Header("Behaviour")]
    public bool quickReset = false;
    public float endDelay = 0f;
    public LockOn lockOn;
    public float playerMarkerWarningTime = 1f;
    public bool nextAttackIsWindUp = false;

    Animator animator;
    BossAI bossAI;
    PlayerReferenceProvider playerRefProvider;
    GameObject playerMarker;
    GameObject rootParent;    
    Vector3 dashTarget;


    void OnEnable()
    {
        rootParent = transform.root.gameObject;

        animator = rootParent.GetComponent<Animator>();
        bossAI = rootParent.GetComponent<BossAI>();

        playerRefProvider = rootParent.GetComponent<PlayerReferenceProvider>();
        playerMarker = playerRefProvider.GetPlayerMarker();
        playerMarker.SetActive(true);
        playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.white;
        playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();

        if (quickReset) bossAI.InvokeReset();
        Invoke(nameof(SetPlayerMarkerToRed), playerMarkerWarningTime);
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine()
    {   
        SetAnimState(1);
        SpawnPrefab(windupPrefab, windupAsChild, windupOffset);
        yield return new WaitForSeconds(windupTime);

        RecordTarget();

        if (playerMarker != null)
            playerMarker.SetActive(false);
            
        lockOn.enabled = false;
        SetAnimState(2);
        SpawnPrefab(dashPrefab, dashAsChild, dashOffset);

        yield return StartCoroutine(Dash());


        SpawnPrefab(endPrefab, endAsChild, endOffset);
        yield return new WaitForSeconds(endDelay);

        End();
    }

    void RecordTarget()
    {
        Vector3 playerPos = playerRefProvider.GetPlayerPosition();
        
        Vector3 forwardToPlayer = (playerPos - rootParent.transform.position).normalized;
        
        forwardToPlayer.y = 0;
        forwardToPlayer.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forwardToPlayer).normalized;

        Vector3 worldOffset = forwardToPlayer * dashPositionOffset.z
                            + Vector3.up * dashPositionOffset.y
                            + right * dashPositionOffset.x;

        dashTarget = playerPos + worldOffset;

        if (useFixedY)
            dashTarget.y = fixedYPosition;
        else
            dashTarget.y = rootParent.transform.position.y;
    }

    IEnumerator Dash()
    {
        Transform mover = rootParent.transform;

        if ((dashTarget - mover.position).sqrMagnitude < 0.01f)
            dashTarget = mover.position + mover.forward * 2f;

        while (true)
        {
            float step = dashSpeed * Time.deltaTime;

            mover.position = Vector3.MoveTowards(mover.position, dashTarget, step);

            if (Vector3.Distance(mover.position, dashTarget) <= stopDistance)
                break;

            yield return null;
        }

        mover.position = dashTarget;
    }

    void SpawnPrefab(GameObject prefab, bool asChild, Vector3 offset)
    {
        if (prefab == null)
            return;

        Transform root = rootParent.transform;

        // Convert local offset into world position
        Vector3 worldPos = root.TransformPoint(offset);

        // Final rotation = boss rotation + prefab rotation as offset
        Quaternion finalRot = root.rotation * prefab.transform.rotation;

        GameObject spawned = Instantiate(prefab, worldPos, finalRot);

        if (asChild)
            spawned.transform.SetParent(root, true);
    }

    void SetAnimState(int value)
    {
        if (animator == null || string.IsNullOrEmpty(animationName))
        {
            return;
        }

        animator.SetInteger(animationName, value);
    }

    void SetPlayerMarkerToRed()
    {
        playerMarker.GetComponent<ChangeEffectColor>().effectColor = Color.red;
        playerMarker.GetComponent<ChangeEffectColor>().ApplyColorToChildren();
    }

    void End()
    {
        if (!quickReset) bossAI.InvokeReset();
        lockOn.enabled = true;

        if (!nextAttackIsWindUp) SetAnimState(0);
        else SetAnimState(1);

        gameObject.SetActive(false);
    }


    public void CancelAndEnd()
    {
        StopAllCoroutines();
        End();
    }
}
