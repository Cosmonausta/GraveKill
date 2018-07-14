using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        moveVelocity = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
}
