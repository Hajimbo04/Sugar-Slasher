using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour
{
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
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, rotation);

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (bulletRig != null)
        {
            bulletRig.linearVelocity = shootDir * bulletSpeed;
        }
        
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.owner = this.gameObject; 
        }

        Destroy(bulletObj, 5f);
    }


}
