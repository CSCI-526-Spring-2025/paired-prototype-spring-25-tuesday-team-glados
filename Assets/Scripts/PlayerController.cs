using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public GameObject eye;  // Reference to the Eye GameObject

    private Rigidbody2D rb;
    private Renderer spriteRenderer;
    private Vector2 eyeLeftMovement = new Vector2(-0.2f, 0.5f);
    private Vector2 eyeRightMovement = new Vector2(0.2f, 0.5f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)
        {
            eye.transform.localPosition = eyeRightMovement;
        }
        else if (moveInput < 0)
        {
            eye.transform.localPosition = eyeLeftMovement;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    bool IsGrounded()
    {
        const float maxDistance = 1f;                   // Maximum distance for the raycast
        float height = spriteRenderer.bounds.size.y;    // Get the height of the GameObject (Y-axis of its bounds)
        Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - (height / 2) - 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, maxDistance);
        return hit.collider != null && hit.distance == 0;
    }
}
