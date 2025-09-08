using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    // The prefab you want to spawn when the enemy is destroyed.
    public GameObject itemToDrop;

    // This function is called just before the game object is destroyed.
    void OnDestroy()
    {
        // Check to make sure an item has been assigned in the Inspector.
        if (itemToDrop != null)
        {
            // Instantiate the prefab at the position of the enemy.
            Instantiate(itemToDrop, transform.position, Quaternion.identity);

            // Log a message to the console to confirm the item was spawned.
            Debug.Log("Spawned " + itemToDrop.name + " at " + transform.position);
        }
        else
        {
            // Log a warning if no item has been assigned.
            Debug.LogWarning("No item assigned to drop on " + gameObject.name);
        }
    }
}