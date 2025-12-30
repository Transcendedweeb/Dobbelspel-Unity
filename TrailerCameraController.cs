using UnityEngine;

public class TrailerCameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float fastMoveMultiplier = 3f;

    [Header("Rotation")]
    public float mouseSensitivity = 3f;
    public bool lockCursor = true;

    [Header("Time Control")]
    public float timeStep = 0.25f;
    public float minTimeScale = 0.1f;
    public float maxTimeScale = 2f;

    [Header("Pause & Frame Control")]
    public KeyCode pauseKey = KeyCode.Space;
    public KeyCode frameStepKey = KeyCode.Period;
    private bool isPaused = true;

    private float rotationX = 0f;
    private float lastReportedSpeed;
    private float lastReportedTimeScale;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Time.timeScale = 0f;
        lastReportedTimeScale = Time.timeScale;

        Debug.Log($"[TrailerCamera] Scene paused. TimeScale = {Time.timeScale}");
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleTimeControl();
        HandlePauseAndFrameStep();
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMoveMultiplier;

        if (!Mathf.Approximately(speed, lastReportedSpeed))
        {
            Debug.Log($"[TrailerCamera] Camera speed: {speed}");
            lastReportedSpeed = speed;
        }

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.E))
            move += transform.up;

        if (Input.GetKey(KeyCode.Q))
            move -= transform.up;

        transform.position += move * speed * Time.unscaledDeltaTime;
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.unscaledDeltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f);

        transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y + mouseX, 0f);
    }

    void HandleTimeControl()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Time.timeScale = Mathf.Max(minTimeScale, Time.timeScale - timeStep);
            LogTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Time.timeScale = Mathf.Min(maxTimeScale, Time.timeScale + timeStep);
            LogTimeScale();
        }
    }

    void HandlePauseAndFrameStep()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;

            Debug.Log(isPaused
                ? "[TrailerCamera] Scene paused"
                : "[TrailerCamera] Scene resumed");

            LogTimeScale();
        }

        if (isPaused && Input.GetKeyDown(frameStepKey))
        {
            Debug.Log("[TrailerCamera] Step one frame");
            StartCoroutine(StepOneFrame());
        }
    }

    void LogTimeScale()
    {
        if (!Mathf.Approximately(Time.timeScale, lastReportedTimeScale))
        {
            Debug.Log($"[TrailerCamera] Scene speed (TimeScale): {Time.timeScale}");
            lastReportedTimeScale = Time.timeScale;
        }
    }

    System.Collections.IEnumerator StepOneFrame()
    {
        Time.timeScale = 0f;
        yield return new WaitForEndOfFrame();
    }
}
