using FirstGearGames.SmoothCameraShaker;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage;
    public bool isParriable = false;
    public GameObject owner;
    public Vector3 initialVelocity;
    private Rigidbody rb;
    public float speed = 20f;
    
    private Renderer rend;

    public ShakeData hitShakeData;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb != null)
            rb.linearVelocity = transform.forward * speed;

        if (Random.value <= 0.2f)
        {
            isParriable = true;
            if (rend != null)
                rend.material.color = Color.red;
        }
        else
        {
            if (rend != null)
                rend.material.color = Color.yellow;
        }
        Destroy(gameObject, 10f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CameraShakerHandler.Shake(hitShakeData);
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}