using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // movement speed
    
    public float tiltAngle = 30.0f; // how much the plane tilts 
    public float tiltSpeed = 5.0f; // how fast the plane tilts

    private Camera mainCam;
    private float camToPlayerDist;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main; // get main camera
        camToPlayerDist = Mathf.Abs(mainCam.transform.position.y - transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(moveX, 0.0f, moveY).normalized;
        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        
        Vector3 pos = transform.position; // clamp to to camera view
        
        // convert camera edges to world positions
        Vector3 minScreenBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, camToPlayerDist));
        Vector3 maxScreenBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 1, camToPlayerDist));
        
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.z = Mathf.Clamp(pos.z, minScreenBounds.z, maxScreenBounds.z);
        
        transform.position = pos;
        
        // tilt player
        float targetTilt = -moveX * tiltAngle;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetTilt);
        
        // smoothly tilts towards target direction
        transform.rotation = Quaternion.Lerp(
            currentRotation,
            targetRotation,
            Time.deltaTime * tiltSpeed);
    }
}
