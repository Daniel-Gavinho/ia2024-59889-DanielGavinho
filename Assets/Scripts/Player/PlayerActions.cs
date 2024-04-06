using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("References")]
    public Transform cameraOrientation;
    public GameObject ShootFrom;
    public LineRenderer lr;
    public LayerMask grappleLayer;

    [Header("Variables")]
    public bool hasGrapple = false;
    private Rigidbody rb;
    private Transform grapplePoint;
    private bool grappling;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grappling = false;
    }

    void Update()
    {
        if(!hasGrapple) return;

        if(Input.GetButtonDown("Fire2"))
        {
            grapple();
        }

        if(Input.GetButtonUp("Fire2"))
        {
            rb.useGravity = true;
            lr.enabled = false;
            grappling = false;
        }
    }

    private void FixedUpdate()
    {
        if(grappling)
        {
            pullPlayerTo(grapplePoint);
        }
    }

    private void LateUpdate()
    {
        if(grappling)
        {
            lr.SetPosition(0, ShootFrom.transform.position);
            lr.SetPosition(1, grapplePoint.transform.position);
        }
    }

    private void grapple() {
        RaycastHit hit;
        Debug.Log("Attempting to grapple");
        Debug.DrawRay(cameraOrientation.position, cameraOrientation.forward * 25, Color.red, 2);
        if (Physics.Raycast(cameraOrientation.position, cameraOrientation.forward, out hit, 25, grappleLayer))
        {
            rb.velocity = Vector3.zero;
            lr.enabled = true;
            lr.SetPosition(1, hit.transform.position);
            grapplePoint = hit.transform;
            rb.useGravity = false;
            grappling = true;
        }
    }

    private void pullPlayerTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + target.up * 1.5f, 20 * Time.deltaTime);
    }
}
