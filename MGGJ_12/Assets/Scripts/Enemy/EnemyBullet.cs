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
        rb = GetComponent<Rigidbody>();
        
        if (rb != null)
            rb.linearVelocity = transform.forward * speed;

        if (Random.value <= 0.2f)
        {
            isParriable = true;
            damage *= 2f; //double the damage of red bullets
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
        Debug.Log($"Bullet hit: {other.gameObject.name}, Tag: {other.tag}"); 
        
        if (other.gameObject == owner) return;
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player! Applying damage...");
            CameraShakerHandler.Shake(hitShakeData);
            other.GetComponent<PlayerStats>()?.TakeDamage(damage);
            
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy! Applying damage...");
            var atm = other.GetComponentInParent<AttributesManager>();
            
            if (atm != null)
            {
                atm.TakeDamage(Mathf.RoundToInt(damage));
                Debug.Log($"Enemy {atm.transform.root.name} took {damage} damage.");
            }
            else
            {
                Debug.LogWarning("No Attributes Manager found on enemy parent.");
            }
            Destroy(gameObject);
            return;
        }
    }
}