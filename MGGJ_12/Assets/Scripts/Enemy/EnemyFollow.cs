using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour
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

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;
        bulletTime = timer;

        Vector3 shootDir = (player.position - spawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(shootDir);

        // Instantiate the bullet VFX and destroy it after 2 seconds
        if (shootEffectVFX != null)
        {
            GameObject vfxInstance = Instantiate(shootEffectVFX, vfxSpawnPoint.position, rotation);
            Destroy(vfxInstance, 2f); // Adjust this value based on your VFX duration
        }
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, rotation);

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (bulletRig != null)
        {
            bulletRig.linearVelocity = shootDir * bulletSpeed;
        }

        // Changed 'Bullet' to 'enemyBullet' because the class name 'Bullet' may not be correct
        // Depending on your project. Using the GameObject variable name is more reliable.
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.owner = this.gameObject;
        }

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