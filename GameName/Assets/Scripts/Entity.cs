using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected virtual void Start(){
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die(){
        Destroy(gameObject);
    }
        
    protected int maxHealth = 100;
    protected int currentHealth;
}