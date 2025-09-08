using UnityEngine;

public class Bullet_Shoot_2 : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;        // The bullet prefab
    public Transform bulletSpawner;        // The spawn point of the bullet
    public float bulletSpeed = 20f;        // Speed of each bullet
    public float bulletLifetime = 2f;      // Time before bullets despawn

    public GameObject muzzleFlashVFX;


    [Header("Shooting Settings")]
    public float fireRate = 0.5f;          // Delay between triple shots
    private float nextFireTime = 0f;

    void Update()
    {
        // Right-click shooting
        if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
        {
            ShootTripleBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootTripleBullet()
    {
        // Middle bullet → straight forward
        SpawnBullet(bulletSpawner.rotation);

        // Left bullet → rotate -45° relative to the spawner's local Y-axis
        Quaternion leftRotation = bulletSpawner.rotation * Quaternion.Euler(0, -45f, 0);
        SpawnBullet(leftRotation);

        // Right bullet → rotate +45° relative to the spawner's local Y-axis
        Quaternion rightRotation = bulletSpawner.rotation * Quaternion.Euler(0, 45f, 0);
        SpawnBullet(rightRotation);
    }

    void SpawnBullet(Quaternion bulletRotation)
    {

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.projectile);
        }
        // Create the bullet with the correct rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletRotation);

        if (muzzleFlashVFX != null)
        {
            // Instantiate the VFX at the same position and rotation as the bullet.
            // A common practice is to destroy the VFX after a short duration.
            GameObject flash = Instantiate(muzzleFlashVFX, bulletSpawner.position, bulletSpawner.rotation);
            Destroy(flash, 1f); // Adjust the lifetime as needed for your effect.
        }

        // Apply velocity using the bullet's forward direction
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bullet.transform.forward * bulletSpeed;
        }

        // Destroy bullet after lifetime
        Destroy(bullet, bulletLifetime);
    }
}
