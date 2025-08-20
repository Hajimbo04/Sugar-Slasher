using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // follow the player
    public float smoothSpeed = 5.0f; // how fast the camera catches up
    public float fixedY;
    public float fixedZ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = new Vector3(target.position.x, fixedY, target.position.z); // maintains fixed height of camera
        Vector3 smoothedPosition = Vector3.Lerp(
            new Vector3(transform.position.x, fixedY, fixedZ),
            desiredPosition,
            Time.deltaTime * smoothSpeed);
        
        transform.position = smoothedPosition;
        
        //transform.LookAt(target);
    }
}
