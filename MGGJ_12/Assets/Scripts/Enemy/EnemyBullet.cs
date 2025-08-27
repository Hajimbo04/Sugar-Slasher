using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}