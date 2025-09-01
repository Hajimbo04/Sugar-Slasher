using UnityEngine;

// This script makes an enemy follow a predetermined path while shooting at the player.
public class EnemyPath : MonoBehaviour
{
    // Public array of Transforms to serve as waypoints.
    // Drag your waypoint GameObjects into this array in the Inspector.
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    // A reference to the player's Transform.
    // The enemy will shoot at this target.
    public Transform player;
    
    // The speed at which the enemy moves along the path.
    public float moveSpeed = 5f;
    
    // The distance at which the enemy will consider a waypoint "reached" and move to the next.
    public float waypointReachedDistance = 0.5f;

    [SerializeField] private float timer = 5f;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed = 500f;

    void Start()
    {
        // Find the player by its tag if not already assigned in the Inspector.
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        
        // Initialize the bullet timer.
        bulletTime = timer;
    }

    void Update()
    {
        // Handle the enemy's movement along the path.
        FollowPath();
        
        // Handle the enemy's shooting at the player.
        ShootAtPlayer();
    }

    // This method handles the enemy's movement along the defined path.
    void FollowPath()
    {
        // Check if waypoints exist and if we haven't reached the end of the path.
        if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
        {
            // If the path is complete, we can stop or loop back to the start.
            // For now, we'll just stop.
            return;
        }

        // Move the enemy towards the current waypoint.
        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[currentWaypointIndex].position,
            moveSpeed * Time.deltaTime
        );

        // Check if the enemy has reached the current waypoint.
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointReachedDistance)
        {
            // Move to the next waypoint in the array.
            currentWaypointIndex++;
            
            // If you want the path to loop, you can add this line:
            // currentWaypointIndex %= waypoints.Length;
        }
    }

    // This method handles the logic for shooting a bullet at the player.
    void ShootAtPlayer()
    {
        // Only shoot if a player target exists.
        if (player == null) return;
        
        // Look at the player, so the bullets are aimed correctly.
        transform.LookAt(player);
        
        // Decrease the timer for the next shot.
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;
        
        // Reset the timer.
        bulletTime = timer;

        // Calculate the direction from the spawn point to the player.
        Vector3 shootDir = (player.position - spawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(shootDir);
        
        // Instantiate the bullet with the correct rotation.
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, rotation);

        // Get the Rigidbody component and set its velocity.
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (bulletRig != null)
        {
            bulletRig.linearVelocity = shootDir * bulletSpeed;
        }
        
        // Destroy the bullet after a set time.
        Destroy(bulletObj, 5f);
    }
}
