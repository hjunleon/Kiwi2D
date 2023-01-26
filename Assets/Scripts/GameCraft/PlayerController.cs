using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 3f;
    public float playerFeetRadius = 0.2f;
    private float direction = 0f;
    private bool isGrounded = false;

    public SpriteRenderer spi;
    public Animator playerAnimator;
    public Transform playerFeet;
    public LayerMask groundLayer;
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to rigidbody component for left right movement and jumping
        playerRb = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();
        //spi = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get direction keypress from user
        direction = Input.GetAxisRaw("Horizontal");

        //Move the player
        if (direction != 0)
        {
            playerRb.velocity = new Vector2(direction * speed, playerRb.velocity.y);
            playerAnimator.SetBool("isRoll", true);
            //transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        } else
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            playerAnimator.SetBool("isRoll", false);
        }

        //Character to face correct direction
        if (direction > 0) //moving right
        {
            //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            spi.flipX = true;
        } else if (direction < 0) //moving left
        {
            //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            spi.flipX = false;

        }

        //Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(playerFeet.position, playerFeetRadius, groundLayer);
        //Debug.Log("isGrounded? " + isGrounded);

        bool hasPressedJump = Input.GetButtonDown("Jump");

        //Debug.Log("Input.GetButtonDown(Jump): " + hasPressedJump);
        //Handle player jumping, player jumps when jump key is pressed and its not midair
        if (hasPressedJump && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }
}
