using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 20f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private bool isJumping;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerSpeed;

    // platform branch added code for respawn
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    void Start()
    {
        // platform branch add respawn point
        respawnPoint = transform.position;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y * 0.5f);
        }

        Flip();

        // platform branch
        fallDetector.transform.position = 
        new Vector2(transform.position.x, fallDetector.transform.position.y);
    }
    
    // platform branch add check if collision was fall detector or checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
 
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}