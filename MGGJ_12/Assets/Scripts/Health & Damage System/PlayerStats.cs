using UnityEngine;
using UnityEngine.SceneManagement; // <-- You need this for SceneManager


public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.12f;
    [SerializeField] private float shakeIntensityPerDamage = 0.02f;

    public GameObject damageVFX;

    public HealthBar healthBar;
    private float currentHealth;

    public string gameOverSceneName = "GameOver";

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    private void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth <= 0) Die();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
        if (cameraShake) cameraShake.Shake(amount * shakeIntensityPerDamage, shakeDuration);
        // Check if a VFX prefab has been assigned.
        if (damageVFX != null)
        {
            // Instantiate the VFX at the player's position.
            GameObject vfx = Instantiate(damageVFX, transform.position, Quaternion.identity);

            // It's good practice to destroy the VFX after it's finished playing.
            Destroy(vfx, 2f); // Adjust the lifetime as needed.
        }

    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died :p");
        SceneManager.LoadScene(gameOverSceneName);
    }
}
