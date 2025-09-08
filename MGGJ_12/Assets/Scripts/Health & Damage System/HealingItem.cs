using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public float healAmount = 5f;
    public float moveSpeed = 2f; // Adjust this value to control the speed.

    public GameObject healVFX;


    void Update()
    {
        // Move the item along the negative Z-axis.
        // The negative Z-axis is typically "forward" in a 3D game.
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerStats component from the other GameObject.
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            // Check if the component exists to prevent errors.
            if (playerStats != null)
            {
                playerStats.HealPlayer(healAmount);
                // Destroy the item after it has been picked up.
                Destroy(gameObject);
            }
        }
    }
    // OnDestroy is called when the GameObject is being destroyed.
    private void OnDestroy()
    {
        if (AudioManager.instance != null)
        {
            // Call the PlaySFX method and pass the collectable audio clip.
            AudioManager.instance.PlaySFX(AudioManager.instance.collectable);
        }
        // Check if a VFX prefab has been assigned to the script.
        if (healVFX != null)
        {
            // Instantiate the VFX at the item's last position.
            GameObject vfx = Instantiate(healVFX, transform.position, Quaternion.identity);

            // Destroy the VFX after a short duration to clean up the scene.
            Destroy(vfx, 2f);
        }
    }
}