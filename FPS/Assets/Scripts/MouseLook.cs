using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Rotation")]
    public Transform playerBody;
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;

    public Transform weaponHolder;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //camera movement.
        HandleCameraMovement();
    }

    /// <summary>
    /// function that handle FPS camera that follows player
    /// Created by: NghiaDC (4/6/2022)
    /// </summary>
    private void HandleCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80.0f, 75.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        weaponHolder.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);

        
    }
}
