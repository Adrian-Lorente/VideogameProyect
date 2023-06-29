using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour{
    // Update is called once per frame
    [SerializeField] private float MAX_SPEED = 7f;
    [SerializeField] private float MAX_JUMPING_SPEED = 16f;
    [SerializeField] Rigidbody2D rigidBody2D;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private bool facingRight = true;
    private float horizontalInput;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        FlipHero();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, MAX_JUMPING_SPEED);
        }

        if (Input.GetButtonUp("Jump") && rigidBody2D.velocity.y > 0f)
        {
            rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, rigidBody2D.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FlipHero()
    {
        if (facingRight && horizontalInput < 0f || !facingRight && horizontalInput > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate() 
    {
        rigidBody2D.velocity = new Vector2(horizontalInput * MAX_SPEED, rigidBody2D.velocity.y);
    }
}
