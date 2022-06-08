using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    public Transform player;
    public Transform cam;

    public float xSensitivity;
    public float ySensitivity;

    public float maxAngle;

    [Header("Set rotation origin for Camera")]
    private Quaternion camCenter;

    public static bool cursorLocked = true;

    void Start()
    {
        camCenter = cam.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }

    private void SetY()
    {
        float inputY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adjustment = Quaternion.AngleAxis(inputY, -Vector3.right);
        Quaternion delta = cam.localRotation * adjustment;

        if(Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            cam.localRotation = delta;
        }
    }

    private void SetX()
    {
        float inputX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adjustment = Quaternion.AngleAxis(inputX, Vector3.up);
        Quaternion delta = player.localRotation * adjustment;

        player.localRotation = delta;
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
}
