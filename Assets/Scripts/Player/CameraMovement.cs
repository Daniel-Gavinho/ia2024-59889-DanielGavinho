using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform cameraOrientation;

    void Update()
    {
        transform.position = cameraOrientation.position;
    }
}
