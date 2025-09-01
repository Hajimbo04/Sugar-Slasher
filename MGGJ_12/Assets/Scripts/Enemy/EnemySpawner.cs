using UnityEngine;

// This script should be attached to a 3D cube or any other object that you want to be a trigger.
// It will "spawn" pre-placed enemies by activating them when the player enters its collider.
public class EnemySpawner : MonoBehaviour
{
    // Drag all the enemy GameObjects you want to activate into this array in the Inspector.
    // Ensure these enemies are initially inactive in the Unity Hierarchy.
    public GameObject[] enemiesToSpawn;

    // Start is called before the first frame update.
    void Start()
    {
        // First, we'll make sure all enemies are hidden at the start of the game.
        // It's a good practice to also have them inactive in the Unity Hierarchy.
        foreach (GameObject enemy in enemiesToSpawn)
        {
            // Set each enemy to inactive so they are hidden from view.
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }

    // OnTriggerEnter is called when another collider enters this object's trigger collider.
    // The collider on your cube MUST have "Is Trigger" checked in the Inspector.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player.
        // We use the "Player" tag for this. Make sure your player object has this tag.
        if (other.CompareTag("Player"))
        {
            // Loop through the array of enemies.
            foreach (GameObject enemy in enemiesToSpawn)
            {
                // Activate each enemy to make them visible and active in the game.
                if (enemy != null)
                {
                    enemy.SetActive(true);
                }
            }

            // After spawning the enemies, we can disable this script so it only triggers once.
            this.enabled = false;

            // Optional: You could also destroy the cube itself if you want it to disappear.
            // Destroy(gameObject);
        }
    }
}
