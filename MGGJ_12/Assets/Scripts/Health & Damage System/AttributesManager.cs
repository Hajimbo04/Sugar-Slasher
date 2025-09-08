using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public GameObject hitVFXPrefab;
    public GameObject healVFXPrefab;
    public GameObject deathVFXPrefab;


    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} now has {health} HP.");

        // Play the SFX for the enemy being hit by a projectile.
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyProjectile);
        }


        PlayHitVFX();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"{gameObject.name} now has {health} HP after healing.");

        PlayHealVFX();
    }


    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }

    protected virtual void Die() // <- make virtual so subclasses can override
    {
        Debug.Log($"{gameObject.name} has died.");
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.explosion);
        }

        PlayDeathVFX();

        Destroy(gameObject);
    }

    private void PlayHitVFX()
    {
        // Only proceed if a VFX prefab has been assigned in the Inspector.
        if (hitVFXPrefab != null)
        {
            // Instantiate the VFX at the enemy's position.
            GameObject vfx = Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);

            // Destroy the VFX after a short duration to clean up the scene.
            // Adjust the time (e.g., 2f) to match the length of your particle effect.
            Destroy(vfx, 2f);
        }
    }

    private void PlayHealVFX()
    {
        if (healVFXPrefab != null)
        {
            GameObject vfx = Instantiate(healVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2f); // Adjust the lifetime as needed
        }
    }
    private void PlayDeathVFX()
    {
        if (deathVFXPrefab != null)
        {
            // Instantiate the VFX at the object's position just before it's destroyed.
            GameObject vfx = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            // Destroy the VFX after a short duration to clean up the scene.
            Destroy(vfx, 3f);
        }
    }
}
