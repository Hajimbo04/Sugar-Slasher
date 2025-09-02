using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.12f;
    [SerializeField] private float shakeIntensityPerDamage = 0.02f;

    public HealthBar healthBar;
    private float currentHealth;

    public SceneLoader sceneLoader;

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
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died :p");
        sceneLoader.LoadGameOver();
    }
}
