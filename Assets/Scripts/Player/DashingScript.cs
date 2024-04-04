using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingScript : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode DashKey = KeyCode.LeftShift;

    [Header("References")]
    public float dashStrength;
    public float dashDuration;
    public Camera cam;
    public float sprintFov;
    public float dashFov;
    public Transform orientation;

    [Header("State")]
    public bool hasDash = false;
    PlayerMovement playerMovement;
    Rigidbody rb;
    bool canDash;
    bool dashing;

    public bool IsDashing() {
        return dashing;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        canDash = true;
    }

    void Update() {
        if(!hasDash) return;

        if(Input.GetKeyDown(DashKey) && !playerMovement.IsGrounded() && canDash) {
            canDash = false;
            dash();
        }

        if(playerMovement.IsGrounded()) {
            canDash = true;
        }
    }

    private IEnumerator DashCoroutine()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        dashing = true;
        cam.fieldOfView = dashFov;
        rb.AddForce(new Vector3(orientation.forward.x, 0, orientation.forward.z) * dashStrength, ForceMode.Impulse);
        yield return new WaitForSeconds(dashDuration);
        cam.fieldOfView = sprintFov;
        dashing = false;
        rb.useGravity = true;
    }

    private void dash()
    {
        StartCoroutine(DashCoroutine());
    }
}
