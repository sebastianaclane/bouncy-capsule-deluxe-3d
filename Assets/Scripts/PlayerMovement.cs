using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask antiJumpGround;
    [SerializeField] LayerMask bouncyGround;

    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wall;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource antiJumpSound;
    [SerializeField] AudioSource enemyDeathSound;

    float timeSinceLastCall;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>(); 

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticallInput * movementSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump("ground");
            }
            else if (TouchedWall())
            {
                Jump("wall");
            }
            else if (IsOnAntiJumpPlatform())
            {
                Jump("antijumpground");
            }
        }

        timeSinceLastCall += Time.deltaTime;
        // If on bouncy platform and it has been 1 second since the last time the player touched the bouncy platform, play the jump sound again
        if (IsOnBouncyPlatform() && timeSinceLastCall >= 1)
        {
            jumpSound.Play();
            timeSinceLastCall = 0;   // reset timer back to 0
        }
    }

    void Jump(string jumpType)
    {
        if (jumpType == "ground")
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpSound.Play();
        } else if (jumpType == "wall")
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce + 1.5f, rb.velocity.z);
            jumpSound.Play();
        } else if (jumpType == "antijumpground")
        {
            antiJumpSound.Play();
        }         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            // Destroy the parent enemy object
            Destroy(collision.transform.parent.gameObject);
            enemyDeathSound.Play();
            Jump("ground");
        }
    }


    bool IsGrounded()
    {
        // This Unity CheckSphere method to determine if an object is overlapping with the ground layer (the 3rd parameter) to determine if the player has touched an object.
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    bool IsOnAntiJumpPlatform()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, antiJumpGround);
    }

    bool IsOnBouncyPlatform()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, bouncyGround);
    }

    bool TouchedWall()
    {
        return Physics.CheckSphere(wallCheck.position, .5f, wall);
    }

}
