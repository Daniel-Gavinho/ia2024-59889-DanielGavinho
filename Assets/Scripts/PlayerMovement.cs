using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement")]
    public float speed;
    public float sprintSpeed;
    public float groundDrag;
    public float jumpForce;
    public float timeToJump;
    public float airMult;
    public float dashStrength;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform orientation;

    private Transform platformTransform;
    private Vector3 platformLastPosition;
    float currentSpeed;
    Vector3 moveDirection;
    float horizontalMovement;
    float verticalMovement;
    bool canDash;
    Rigidbody rb;
    bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = speed;
        canJumpAgain();
    }

    void Update()
    {
        groundCheck();
        pressedKeys();
        speedLimit();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate() {
        move();
    }

    private void pressedKeys() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(jumpKey) && isGrounded && canJump) {
            jump();

            canJump = false;

            Invoke("canJumpAgain", timeToJump);
        }

        if(Input.GetKey(sprintKey)) {
            currentSpeed = sprintSpeed;
        } else {
            currentSpeed = speed;
        }

        if(Input.GetKeyDown(sprintKey) && !isGrounded && canDash) {
            canDash = false;
            dash();
        }

        if(isGrounded) {
            canDash = true;
        }
    }

    private void move() {
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * currentSpeed * airMult, ForceMode.Force);
    }

    private void groundCheck() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.05f, groundMask);
    }

    private void speedLimit() {
        if(isGrounded) {
            Vector3 groundVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (groundVelocity.magnitude > currentSpeed) {
                Vector3 limitedVelocity = groundVelocity.normalized * currentSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    private void dash() {
        rb.AddForce(orientation.forward * dashStrength, ForceMode.VelocityChange);
    }

    private void jump() {
        rb.drag = 0;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void canJumpAgain() {
        canJump = true;
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            platformTransform = collision.gameObject.transform;
            platformLastPosition = platformTransform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            Vector3 platformPositionDelta = platformTransform.position - platformLastPosition;
            
            rb.MovePosition(rb.position + platformPositionDelta);

            platformLastPosition = platformTransform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving"))
        {
            platformTransform = null;
            rb.drag = 0;
            rb.velocity += collision.gameObject.GetComponent<MovingPlatform>().Velocity;
            Debug.Log("Velocity: " + rb.velocity);
            Debug.Log("Drag: " + rb.drag);
        }
    }
}
