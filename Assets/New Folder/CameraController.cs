using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;
using Unity.Netcode;

public class CameraController : NetworkBehaviour
{
    public Transform target; // Target transform to rotate around
    public float distance = 5f; // Distance from the target
    public float XrotationSpeed = 1f; // Rotation speed of the camera on the X Axis
    public float YrotationSpeed = 1f; // Rotation speed of the camera on the Y Axis
    public float minVerticalAngle = -80f; // Minimum vertical angle of the camera
    public float maxVerticalAngle = 80f; // Maximum vertical angle of the camera

    private float currentHorizontalAngle = 0f; // Current horizontal angle of the camera
    private float currentVerticalAngle = 0f; // Current vertical angle of the camera

    private void LateUpdate()
    {
        if (IsOwner)
        {
            // Get the mouse input for rotation
            float mouseX = Input.GetAxis("Mouse X") * XrotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * YrotationSpeed;

            // Update the current horizontal and vertical angles based on input
            currentHorizontalAngle += mouseX;
            currentVerticalAngle -= mouseY;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

            // Calculate the rotation based on the angles
            Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);

            // Calculate the desired position based on the rotation and distance
            Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance;

            // Set the camera's position and look at the target
            transform.position = desiredPosition;
            transform.LookAt(target.position);
        }
        
    }
}
