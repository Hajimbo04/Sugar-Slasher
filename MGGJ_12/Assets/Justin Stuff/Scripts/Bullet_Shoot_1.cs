using UnityEngine;

public class Bullet_Shoot_1 : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;      // The bullet prefab
    public Transform bulletSpawner;      // The spawn point of the bullet
    public float bulletSpeed = 20f;      // Speed of the bullet
    public float bulletLifetime = 2f;    // Time before bullet despawns

    [Header("Shooting Settings")]
    public float fireRate = 0.2f;        // Delay between shots
    private float nextFireTime = 0f;     // Time until next shot allowed

    void Update()
    {
        // Check if player presses Left Mouse Button or Left Ctrl
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet()
    {
        // Instantiate bullet at the spawner position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);

        // Get Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Move bullet forward along its local Z-axis
            rb.linearVelocity = bulletSpawner.forward * bulletSpeed;
        }

        // Destroy bullet after lifetime
        Destroy(bullet, bulletLifetime);
    }
}
