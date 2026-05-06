using UnityEngine;
using Pathfinding;

public class Enemy : Entity
{
    [Header("Attack")]
    public int attackDamage = 20;
    public float attackCooldown = 1f;
    public float attackRadius = 4.5f;

    private float lastAttackTime;
    private Transform player;
    private AIPath aiPath;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").transform;
        aiPath = GetComponent<AIPath>();

        if (player == null)
        Debug.LogError("Enemy cannot find Player! Is the tag set correctly?");
    if (aiPath == null)
        Debug.LogError("Enemy has no AIPath component!");
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            aiPath.canMove = false; 

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                var entityComponent = player.GetComponent<Entity>();
                Debug.Log("Entity component found: " + (entityComponent != null));
                player.GetComponent<Entity>()?.TakeDamage(attackDamage);
                Debug.Log("Enemy attacked player for " + attackDamage + " damage");
            }
        }
        else
        {
            aiPath.canMove = true;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}