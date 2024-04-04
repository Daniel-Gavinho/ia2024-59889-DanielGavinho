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
    public float maxAirSpeed;
    public float groundDrag;
    public float jumpForce;
    public float timeToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;

    [Header("Camera")]
    public Camera cam;
    public float sprintFov;

    bool isGrounded;

    public Transform orientation;

    private Transform platformTransform;
    private Vector3 platformLastPosition;
    float currentSpeed;
    Vector3 moveDirection;
    float horizontalMovement;
    float verticalMovement;
    Rigidbody rb;
    bool canJump;
    bool momentumJump;
    DashingScript dashScript;

    public bool IsGrounded() {
        return isGrounded;
    }

    void Start()
    {
        dashScript = GetComponent<DashingScript>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = sprintSpeed;
        cam.fieldOfView = sprintFov;
        canJumpAgain();
    }

    void Update()
    {
        groundCheck();
        pressedKeys();
        speedLimit();

        if (isGrounded) {
            rb.drag = groundDrag;
            momentumJump = false;
        }
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

        if(isGrounded) {
            if(Input.GetKey(sprintKey)) {
                currentSpeed = speed;
            } else {
                currentSpeed = sprintSpeed;
                cam.fieldOfView = sprintFov;
            }
        }
    }

    private void move() {
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Force);
    }

    private void groundCheck() {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, 3*playerHeight/8, Vector3.down, out hit, playerHeight/4, groundMask);
        if(isGrounded) Debug.Log("Grounded: " + isGrounded);
    }

    private void speedLimit() {
        if(isGrounded) {
            Vector3 groundVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (groundVelocity.magnitude > currentSpeed) {
                Vector3 limitedVelocity = groundVelocity.normalized * currentSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        } else if (!momentumJump && !dashScript.IsDashing()) {
            Vector3 airVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Vector3 verticalVelocity = new Vector3(0, rb.velocity.y, 0);

            if (airVelocity.magnitude > maxAirSpeed) {
                Vector3 limitedVelocity = airVelocity.normalized * currentSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }

            if(verticalVelocity.magnitude > maxAirSpeed) {
                Vector3 limitedVelocity = verticalVelocity.normalized * maxAirSpeed;
                rb.velocity = new Vector3(rb.velocity.x, limitedVelocity.y, rb.velocity.z);
            }
        }
    }

    private void jump() {
        rb.drag = 0;
        currentSpeed = maxAirSpeed;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void canJumpAgain() {
        canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
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
            momentumJump = true;
            rb.drag = 0;
            rb.velocity += collision.gameObject.GetComponent<MovingPlatform>().Velocity;
            Debug.Log("Velocity: " + rb.velocity);
            Debug.Log("Drag: " + rb.drag);
        }
    }
}
