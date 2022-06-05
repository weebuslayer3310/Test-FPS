using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Weapon Sway")]
    
    public float intensity = 5.0f;
    public float smooth = 10.0f;
    private Quaternion originRotation;


    private void Start()
    {
        originRotation = transform.rotation;
    }

    private void Update()
    {
        UpdateSway();
    }

    /// <summary>
    /// functiong that adding sway to the gun
    /// </summary>
    public void UpdateSway()
    {
        //controls
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //calculate target rotation
        Quaternion targetAdjustmentX = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
        Quaternion targetAdjustmentY = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
        Quaternion targetRotation = originRotation * targetAdjustmentX * targetAdjustmentY;

        //rotate the gun towards that rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
