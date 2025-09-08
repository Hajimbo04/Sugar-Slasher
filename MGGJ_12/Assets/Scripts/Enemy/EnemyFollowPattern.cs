using UnityEngine;
using System.Collections;

public class EnemyFollowPattern : MonoBehaviour
{
    // The prefab you want to spawn when the enemy is destroyed.
    public GameObject itemToDrop;
    public GameObject shootEffectVFX; 
    public Transform vfxSpawnPoint; // <-- Add this line

    
    public Transform player;
    public Transform targetPoint;
    public float moveSpeed = 5f;

    [SerializeField] private float timer = 5f;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed = 500f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        FollowPlayer();
        ShootAtPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime);

        transform.LookAt(player);
    }

    void ShootInCirclePattern(int numberOfBullets, float radius)
    {
        // The angle between each bullet. 360 degrees divided by the number of bullets.
        float angleStep = 360f / numberOfBullets;
        float currentAngle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the direction vector for the bullet using trigonometry.
            float bulletDirX = Mathf.Sin((currentAngle * Mathf.Deg2Rad));
            float bulletDirZ = Mathf.Cos((currentAngle * Mathf.Deg2Rad));

            Vector3 bulletMoveDirection = new Vector3(bulletDirX, 0, bulletDirZ);
            
            // Instantiate the bullet at the enemy's position with no rotation.
            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            
            // Get the Rigidbody and apply force in the calculated direction.
            Rigidbody bulletRig = bullet.GetComponent<Rigidbody>();
            if (bulletRig != null)
            {
                bulletRig.linearVelocity = bulletMoveDirection * bulletSpeed;
            }

            currentAngle += angleStep;
        }
    }

    IEnumerator ShootSpiralPattern(int totalBullets, float timeBetweenBullets)
    {
        float currentAngle = 0f;
        float angleStep = 15f; // Adjust this value to change how tight or wide the spiral is.

        for (int i = 0; i < totalBullets; i++)
        {
            float bulletDirX = Mathf.Sin((currentAngle * Mathf.Deg2Rad));
            float bulletDirZ = Mathf.Cos((currentAngle * Mathf.Deg2Rad));

            Vector3 bulletMoveDirection = new Vector3(bulletDirX, 0, bulletDirZ);

            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);

            Rigidbody bulletRig = bullet.GetComponent<Rigidbody>();
            if (bulletRig != null)
            {
                bulletRig.linearVelocity = bulletMoveDirection * bulletSpeed;
            }
            
            currentAngle += angleStep;
            
            // Wait for a short duration before shooting the next bullet.
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;
        bulletTime = timer;

        // Call one of the pattern functions instead of the old shooting code.
        ShootInCirclePattern(16, 1f);
        
        // OR start a coroutine for a spiral pattern.
        // Example: StartCoroutine(ShootSpiralPattern(50, 0.05f));
    }

    /// <summary>
    /// This method is called when the GameObject is destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (itemToDrop != null)
        {
            // Instantiate the prefab at the enemy's position.
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
            Debug.Log("Spawned " + itemToDrop.name + " at " + transform.position);
        }
        else
        {
            Debug.LogWarning("No item assigned to drop on " + gameObject.name);
        }
    }
}