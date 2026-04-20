using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform attackPoint;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(attackPoint.position, 0.1f);
    }
    void Update()
    {
        // movement
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // mouse world position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        attackPoint.position = (Vector2)transform.position + direction * 1f;
        Debug.DrawLine(transform.position, attackPoint.position, Color.red);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
