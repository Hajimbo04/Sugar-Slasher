using UnityEngine;

public class EnemyDestroyerArea : MonoBehaviour
{
    // This function is called when another collider enters the trigger collider attached to this object.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering GameObject has the "Enemy" tag.
        if (other.CompareTag("Enemy"))
        {
            // Destroy the GameObject that entered the trigger.
            // other.gameObject refers to the GameObject with the collider that entered the trigger.
            Destroy(other.gameObject);
        }
    }
}