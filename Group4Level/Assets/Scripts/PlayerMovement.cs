using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;          //player speed
    private float jumpingPower = 8f;   //player jumping power
    private bool isFacingRight = true;
    private bool isJumping;
    private bool IsGrounded;
    private Animator anim;
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
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        //horizontal movement
        horizontal = Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;
        

        //lets player jump if player touching ground
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //lets player jump once (can't double/triple jump)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
        }
        
        //if vertical is at 0, jumping animation wont play
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
        }
        //if vertical is > 0, jumping animation plays
        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }
        //if vertical < 0, jumping animation wont play
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
        }
        //if player is moving, running animations plays
        if (rb.velocity.x > 0.1)
        {
            anim.SetBool("isRunning", true);
        }
        //if player is not moving, idle animation plays
        if (rb.velocity.x < 0)
        {
            anim.SetBool("isRunning", false);
        }
        //if player is not moving, idle animation plays
        if (rb.velocity.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        
        //allows player to jump longer
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y * 0.5f);
        }
        
        //calls flip method
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

    //horizontal movement speed
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    

    //lets player look left and right
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

    