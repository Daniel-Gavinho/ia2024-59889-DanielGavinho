using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private static Vector3 FIRST_PERSON = new Vector3(0, 0, 0);
    private static Vector3 THIRD_PERSON = new Vector3(1,0.5f,-2);

    public Transform parent;

    public float senX;
    public float senY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yRotation = -90;
    }

    private void Update()
    {
        CheckKeys();
        yRotation += Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        xRotation -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if(transform.localPosition == THIRD_PERSON)
            parent.rotation = transform.rotation;

    }

    private void CheckKeys() {
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            if(transform.localPosition == THIRD_PERSON)
                transform.localPosition = FIRST_PERSON;
            else
                transform.localPosition = THIRD_PERSON;
                parent.rotation = transform.rotation;
        }
    }

}
