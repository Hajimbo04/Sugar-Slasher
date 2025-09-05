using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} now has {health} HP.");

        if (health <= 0)
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

    protected virtual void Die() // <- make virtual so subclasses can override
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
