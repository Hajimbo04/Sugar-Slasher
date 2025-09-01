using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} now has {health} HP.");
        
        if (health <= 0) // ✅ check if enemy is dead
        {
            Die();
        }
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }
    
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died."); 
        Destroy(gameObject); 
    }
}