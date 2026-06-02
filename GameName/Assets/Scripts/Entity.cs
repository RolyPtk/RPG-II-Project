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

    public int maxHealth = 100;
    protected int currentHealth;

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }
}