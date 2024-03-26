using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("References")]
    public Transform cameraOrientation;
    public GameObject BulletPrefab;
    public GameObject ShootFrom;
    public LineRenderer lr;

    [Header("Variables")]
    private Rigidbody rb;
    private int ammo = 10;
    private Vector3 grapplePoint;
    private bool grappling;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grappling = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }

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
        }
    }

    private void reload()
    {
        ammo = 10;
    }

    private void shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            Instantiate(BulletPrefab, transform.position + cameraOrientation.forward + new Vector3(0, 0.5f, 0), cameraOrientation.rotation);
        }
    }

    private void grapple() {
        RaycastHit hit;
        if (Physics.Raycast(cameraOrientation.position, cameraOrientation.forward, out hit, 100))
        {
            if (hit.transform.tag == "Grappleable")
            {
                lr.enabled = true;
                lr.SetPosition(1, hit.point);
                grapplePoint = hit.point + hit.transform.up/2;
                rb.useGravity = false;
                grappling = true;
            }
        }
    }

    private void pullPlayerTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 20 * Time.deltaTime);
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
        guiStyle.fontSize = 20;
        guiStyle.alignment = TextAnchor.MiddleRight;

        float labelWidth = 200f;
        float labelHeight = 30f;
        float padding = 10f;
        float labelX = Screen.width - labelWidth - padding;
        float labelY = Screen.height - labelHeight - padding;

        Rect labelRect = new Rect(labelX, labelY, labelWidth, labelHeight);

        GUI.Label(labelRect, "Ammo: " + ammo, guiStyle);
    }


}
