using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : AttributesManager
{
    [Header("Drop Health Item Settings")]
    [Range(0f, 1f)] public float dropChance = 0.1f; // 10% chance
    public GameObject healthPickupPrefab;

    protected override void Die()
    {


        ScoreManager.Instance.AddScore(100);

        if (healthPickupPrefab != null && Random.value <= dropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
