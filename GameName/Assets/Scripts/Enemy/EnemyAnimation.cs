using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private AIPath aiPath;

    void Start()
    {
        animator = GetComponent<Animator>();
        aiPath = GetComponentInParent<AIPath>();
    }

    void Update()
    {
        Vector2 velocity = aiPath.desiredVelocity;

        animator.SetFloat("MoveX", velocity.x);
        animator.SetFloat("MoveY", velocity.y);
        animator.SetFloat("Speed", velocity.magnitude);
    }

    public void Attack()
    {
        Debug.Log("ATTACK TRIGGER");

        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }
}