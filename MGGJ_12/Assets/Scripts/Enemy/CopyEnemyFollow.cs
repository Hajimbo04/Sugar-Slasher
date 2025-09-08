using UnityEngine;
using System.Collections;

public class CopyEnemyFollow : MonoBehaviour
{
    // The prefab you want to spawn when the enemy is destroyed.
    public GameObject itemToDrop;
    public GameObject shootEffectVFX; 
    public Transform vfxSpawnPoint; // <-- Add this line

    
    public Transform target;

    public Transform targetPoint;
    public float moveSpeed = 5f;

    [SerializeField] private float timer = 5f;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed = 500f;

    void Start()
    {
        // if (player == null)
        // {
        //     player = GameObject.FindWithTag("Player").transform;
        // }
    }

    void Update()
    {
        FollowPlayer();
        ShootAtPlayer();
    }

    void FollowPlayer()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position, // Replaced player.position
            moveSpeed * Time.deltaTime);

        transform.LookAt(target); // Replaced player
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;
        bulletTime = timer;

        if (target == null) return; // Always good to have this safety check

        // Use a single, reliable spawn point for both direction and position
        // We'll use vfxSpawnPoint since that's where the visual effect should originate.
        Vector3 shootDir = (target.position - vfxSpawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(shootDir);

        // Instantiate the bullet VFX first
        if (shootEffectVFX != null)
        {
            GameObject vfxInstance = Instantiate(shootEffectVFX, vfxSpawnPoint.position, rotation);
            Destroy(vfxInstance, 2f); 
        }

        // Now instantiate the actual bullet
        GameObject bulletObj = Instantiate(enemyBullet, vfxSpawnPoint.position, rotation); 

        // Apply velocity to the bullet
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (bulletRig != null)
        {
            bulletRig.linearVelocity = shootDir * bulletSpeed;
        }

        // Set the bullet's owner
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.owner = this.gameObject;
        }

        // Destroy the bullet after a set time
        Destroy(bulletObj, 5f);
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