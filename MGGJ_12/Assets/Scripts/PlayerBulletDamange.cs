using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 5; // bullet damage

    private void OnTriggerEnter(Collider other)
    {
        // Only damage enemies
        if (other.CompareTag("Enemy"))
        {
            AttributesManager atm = other.GetComponent<AttributesManager>();
            atm.TakeDamage(damage);

            if (atm.health <= 0)
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
