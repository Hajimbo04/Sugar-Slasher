using UnityEngine;

// This script should be attached to your enemy cylinder GameObject.
public class H_EnemyStatic : MonoBehaviour
{
    // A reference to the bullet prefab.
    // Drag your bullet prefab into this slot in the Inspector.
    public GameObject bulletPrefab;

    // A reference to the point where bullets will spawn.
    // Create an empty GameObject as a child of the cylinder and drag it here.
    public Transform bulletSpawnPoint;
    
    // The rate of fire, in seconds. A value of 2 means a bullet is fired every 2 seconds.
    public float fireRate = 2f;
    private float nextFireTime;

    // The speed of the bullet.
    public float bulletSpeed = 10f;

    // The radius for player detection.
    // This value can be adjusted in the Inspector to control the enemy's range.
    public float detectionRadius = 10f;

    // The speed at which the enemy moves.
    public float moveSpeed = 5f;

    // A public variable for the player's Transform.
    // This allows you to drag the Player prefab into this slot in the Inspector.
    public Transform player;

    // An optional boolean to show or hide the detection radius gizmo in the Scene view.
    public bool showDetectionGizmo = true;
    
    // Start is called before the first frame update.
    void Start()
    {
        // Check if the player has been assigned in the Inspector.
        if (player == null)
        {
            // If not, find the player by its tag. This is a fallback to prevent errors.
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                // If the player still can't be found, log an error.
                Debug.LogError("Player GameObject not found! Please ensure your player has the 'Player' tag or is assigned in the Inspector.");
            }
        }

        // Set the initial time for the next shot.
        nextFireTime = Time.time;
    }

    // Update is called once per frame.
    void Update()
    {
        // The enemy now moves continuously, regardless of the player's position.
        // We multiply the forward direction by the speed and the time passed since the last frame.
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        
        // Check if the player exists and is within the detection radius.
        // Vector3.Distance() calculates the straight-line distance between two points.
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // The enemy looks at the player while they are in range.
            transform.LookAt(player);
            
            // Check if it's time to fire another bullet.
            if (Time.time >= nextFireTime)
            {
                // Call the fire method.
                FireBullet();
                // Set the time for the next shot based on the fire rate.
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Fires a bullet from the spawn point and aims it directly at the player.
    void FireBullet()
    {
        // Calculate the direction from the spawn point to the player.
        // This is the key to accurate aiming.
        Vector3 shootDir = (player.position - bulletSpawnPoint.position).normalized;

        // Create a rotation that looks along the shoot direction.
        Quaternion rotation = Quaternion.LookRotation(shootDir);
        
        // Instantiate the bullet with the correct rotation.
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);

        // Get the Rigidbody component from the bullet.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Add a forward force to the bullet using the calculated shoot direction.
        if (rb != null)
        {
            rb.linearVelocity = shootDir * bulletSpeed;
        }

        // Destroy the bullet after 3 seconds to keep the scene clean.
        Destroy(bullet, 3f);
    }
    
    // OnDrawGizmos is used to draw a visual aid in the Scene view.
    void OnDrawGizmosSelected()
    {
        if (showDetectionGizmo)
        {
            // Set the color for the gizmo.
            Gizmos.color = Color.yellow;
            // Draw a wire sphere at the enemy's position with the specified radius.
            // This lets you see the detection range in the Scene view.
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}