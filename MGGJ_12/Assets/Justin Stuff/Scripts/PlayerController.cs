using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // movement speed
    
    public float tiltAngle = 30.0f; // how much the plane tilts 
    public float tiltSpeed = 5.0f; // how fast the plane tilts

    public float parryRadius = 5f;
    public float parryCooldown = 2f;
    public GameObject parryEffectPrefab;

    private float parryTimer = 0f;
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
        
        // deadzone check
        if (Mathf.Abs(moveX) < 0.1f) moveX = 0f;
        if (Mathf.Abs(moveY) < 0.1f) moveY = 0f;
        
        Vector3 moveDirection = new Vector3(moveX, 0.0f, moveY).normalized;
        
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        
        Vector3 pos = transform.position; // clamp to to camera view
        
        // convert camera edges to world positions
        Vector3 minScreenBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, camToPlayerDist));
        Vector3 maxScreenBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 1, camToPlayerDist));
        
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.z = Mathf.Clamp(pos.z, minScreenBounds.z, maxScreenBounds.z);
        
        transform.position = pos;
        
        
        float targetTilt = -moveX * tiltAngle;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetTilt);
        
        transform.rotation = Quaternion.Lerp(
            currentRotation,
            targetRotation,
            Time.deltaTime * tiltSpeed);

        if (parryTimer > 0)
            parryTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.F) && parryTimer <= 0f)
        {
            Parry();
        }
    }

    void Parry()
    {
        if (parryEffectPrefab != null)
        {
            parryEffectPrefab.SetActive(true);
            parryEffectPrefab.transform.localPosition = new Vector3(0f, 0f, -1f);
            parryEffectPrefab.transform.localScale = Vector3.one * parryRadius;
        }
        Invoke("DisableParryEffect", 0.3f);

        
        Collider[] hits = Physics.OverlapSphere(transform.position, parryRadius);
        foreach (Collider hit in hits)
        {
            Bullet bullet = hit.GetComponent<Bullet>();
            if (bullet != null && bullet.isParriable && bullet.owner != null)
            {
                Vector3 directionToEnemy = (bullet.owner.transform.position - bullet.transform.position).normalized;
                bullet.transform.forward = directionToEnemy;
                
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    float speed = rb.linearVelocity.magnitude;
                    rb.linearVelocity = directionToEnemy * speed;
                }
                bullet.isParriable = false;
            }
        }
        parryTimer = parryCooldown;
    }
    
    void DisableParryEffect()
    {
        if (parryEffectPrefab != null)
            parryEffectPrefab.SetActive(false); // hide barrier
    }
}
