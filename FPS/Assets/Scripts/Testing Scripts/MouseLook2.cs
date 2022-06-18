using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook2 : MonoBehaviour
{
    public Transform player;
    public Transform FPScam;
    public Transform weapon;

    public float xSensitivity;
    public float ySensitivity;
    public float maxAngle;

    private Quaternion camCenter;

    public static bool CursorLocked = true;

    private void Start()
    {
        //set rotation for camera to camCenter.
        camCenter = FPScam.localRotation;
    }

    private void Update()
    {
        //get input from mouse Y axis.
        SetY();

        //get input from mouse X axis.
        SetX();

        // hide / un-hide cursor.
        UpdateCursorLock();
    }


    void SetY()
    {
        float inputY = Input.GetAxis("Mouse Y") * ySensitivity;
        Quaternion adjustment = Quaternion.AngleAxis(inputY, -Vector3.right);
        Quaternion delta = FPScam.localRotation * adjustment;

        if(Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            FPScam.localRotation = delta;
            weapon.localRotation = delta;
        }
        weapon.rotation = FPScam.rotation;
    }

    void SetX()
    {
        float inputX = Input.GetAxis("Mouse X") * xSensitivity;
        Quaternion adjustment = Quaternion.AngleAxis(inputX, Vector3.up);
        Quaternion delta = player.localRotation * adjustment;
        player.localRotation = delta;
    }

    void UpdateCursorLock()
    {
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorLocked = true;
            }
        }
    }
}
