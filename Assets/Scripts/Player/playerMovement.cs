using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 1;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius;
    public float jumpForce;

    private Rigidbody2D rb;
    private float horizontalMovement;
    private playerFacing facing = new playerFacing();
    private playerFacing lastFacing = new playerFacing();

    [HideInInspector]
    public enum playerFacing{Left, Right, Up, Down};
    [HideInInspector]
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        facing = playerFacing.Right;
        lastFacing = facing;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        CheckGrounded();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        FacePlayer();
    }

    /// <summary>
    /// Function that reads the input from the user and moves the player accordingly
    /// </summary>
    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (horizontalMovement < 0)
        {
            facing = playerFacing.Left;
        }
        else if (horizontalMovement == 0)
        {
            // nothing happens
        }
        else
        {
            facing = playerFacing.Right;
        }
    }

    /// <summary>
    /// Handles the facing of the player to the direction that the player is moving in, only changes when the player swaps movement directions
    /// </summary>
    private void FacePlayer()
    {
        if (facing != lastFacing)
        {
            switch (facing)
            {
                case playerFacing.Right:
                {
                        transform.localScale = new Vector3(1, 1, 1);
                        Debug.Log("|PLAYERMOVEMENT| ===> The player is now moving to the right, flipping sprite");
                        break;
                }
                case playerFacing.Left:
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        Debug.Log("|PLAYERMOVEMENT| ===> The player is now moving to the left, flipping sprite");
                        break;
                    }
            }
            lastFacing = facing;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("|PLAYERMOVEMENT| ===> Player is grounded -> Jumping");
            rb.velocity = Vector2.up * jumpForce;
        }
        else
        {
            Debug.Log("|PLAYERMOVEMENT| ===> Player is not grounded -> Can't jump");
        }
    }
}
