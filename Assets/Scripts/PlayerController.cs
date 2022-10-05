using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Transform direction;
    Rigidbody rb;

    private PlayerFireGun playerFireGun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerFireGun = GetComponent<PlayerFireGun>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update() 
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();
        SpeedControl();

        if (grounded) 
        {
            rb.drag = groundDrag;
        }
        else 
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        if (playerFireGun.isReloading)
        {
            MovePlayer(moveSpeed * 5);
        }
        else
        {
            MovePlayer(moveSpeed);
        }
    }

    private void MyInput() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded )
        {
            readyToJump = false;

            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer(float moveSpeed) 
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (grounded) 
        {
          rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) 
        {
          rb.AddForce(moveDirection.normalized * airMultiplier * 10f, ForceMode.Force);
        }
    }

    private void SpeedControl() 
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed) 
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() 
    {
        readyToJump = true;
    }
}
