using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    public Transform attackPoint;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
        DrawAttackCone();
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // direcția în care lovești (spre mouse)
            Vector2 attackDirection = (attackPoint.position - transform.position).normalized;

            float angle = Vector2.Angle(attackDirection, directionToEnemy);

            if (angle <= 60f) // 120° total (60° stânga + 60° dreapta)
                enemy.GetComponent<Entity>()?.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    void DrawAttackCone()
    {
        int segments = 20;
        float angleStep = 120f / segments;

        Vector2 attackDirection = (attackPoint.position - transform.position).normalized;
        float startAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg - 60f;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            Debug.DrawLine(transform.position, (Vector2)transform.position + dir * attackRange, Color.red, 0.5f);
        }
    }


}