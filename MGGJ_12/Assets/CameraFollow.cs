using UnityEngine;

public class SubtleCameraFollow : MonoBehaviour
{
    public Transform target;                 // The player to follow
    [Range(0f, 1f)] public float followFactor = 0.25f; // 0 = static, 1 = full follow
    public float smoothSpeed = 3f;           // How quickly the camera eases toward the goal

    private Vector3 baseCamPos;              // Camera position at start
    private Vector3 targetStartPos;          // Player position at start
    private float fixedY;                    // Lock camera height

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("SubtleCameraFollow: No target assigned.");
            enabled = false;
            return;
        }

        baseCamPos     = transform.position; // keep your scene setup
        targetStartPos = target.position;
        fixedY         = transform.position.y; // lock height
    }

    void LateUpdate()
    {
        // How far the player has moved since start (XZ only)
        Vector3 delta = target.position - targetStartPos;

        // Camera only follows a fraction of that movement
        Vector3 desired = baseCamPos + new Vector3(delta.x * followFactor, 0f, delta.z * followFactor);
        desired.y = fixedY; // keep height fixed

        // Smoothly ease toward desired position
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * smoothSpeed);

        // Keep your scene-set rotation (don’t modify transform.rotation)
    }
}