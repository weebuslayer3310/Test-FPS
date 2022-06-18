using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    private void Update()
    {
        GunSway();
    }

    /// <summary>
    /// Get the Gun swaying when moving the mouse 
    /// Created by: NghiaDC (18/6/2022)
    /// </summary>
    void GunSway()
    {
        //controls.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //calculate target rotation.
        Quaternion adjustmentX = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
        Quaternion adjustmentY = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
        Quaternion targetRotation = originRotation * adjustmentX * adjustmentY;

        //rotate towards target rotation.
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
