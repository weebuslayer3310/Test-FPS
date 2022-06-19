using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float speed;
    public float walkingSpeed = 7.0f;
    public float runningSpeed = 10.0f;
    public float jumpHeight = 2.0f;

    [Header("Camera Sprinting")]
    public Camera cam;
    public float baseFOV;
    public float sprintFOVmodifier = 1.25f;

    [Header("PlayerController Gravity")]
    public float gravity = -19.62f;
    Vector3 velocity;

    [Header("Ground check variable")]
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    [Header("weapon bobbing")]
    public Transform weaponParent;
    private Vector3 weaponParentOrigin;
    private float movementCounter;
    private float idleCounter;
    private Vector3 targetWeaponBobPosition;


    void Start()
    {
        baseFOV = cam.fieldOfView;
        weaponParentOrigin = weaponParent.localPosition;
    }
    void Update()
    {
        //handle ground check.
        HandleGroundCheck();

        //handle character movement.
        HandleMovement();

        //handle gravity to our character.
        HandleGravity();
    }


    /// <summary>
    /// function that handle character movement
    /// Created by: NghiaDC (4/6/2022)
    /// </summary>
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //player sprinting and prevent player from running backward.
        if (Input.GetKey(KeyCode.LeftShift) && z > 0)
        {
            //change cameraFOV when running.
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV * sprintFOVmodifier, Time.deltaTime * 8.0f);

            speed = runningSpeed;
        }
        else
        {
            //change the cameraFOV back when walking.
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV, Time.deltaTime * 8.0f);

            speed = walkingSpeed;
        }

        controller.Move(move * speed * Time.deltaTime);

        //head bobbing
        #region Head Bobbing
        if (x == 0 && z == 0)
        {
            HeadBob(idleCounter, 0.0f, 0.01f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2.0f);
        }
        else if(!Input.GetKey(KeyCode.LeftShift) && isGrounded && !Input.GetMouseButton(1))
        {
            HeadBob(movementCounter, 0.1f, 0.05f);
            movementCounter += Time.deltaTime * 5;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6.0f);
        }
        else if (!isGrounded)
        {
            HeadBob(movementCounter, 0.0f, 0.0f);
            movementCounter += Time.deltaTime * 7;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 8.0f);
        }
        else if (Input.GetMouseButton(1))
        {
            HeadBob(movementCounter, 0.0f, 0.01f);
            movementCounter += Time.deltaTime * 7;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 8.0f);
        }
        else if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            HeadBob(movementCounter, 0.1f, 0.07f);
            movementCounter += Time.deltaTime * 7;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 8.0f);
        }
        #endregion


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    /// <summary>
    /// function that handle the character is on grounded or not
    /// Created by: NghiaDC (4/6/2022)
    /// </summary>
    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }
    }

    /// <summary>
    /// function that apply gravity to our character
    /// Created by: NghiaDC (4/6/2022)
    /// </summary>
    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// function that apply headbob while idling, walking and running
    /// Created by: NghiaDC (6/6/2022)
    /// </summary>
    /// <param name="paremeterZ"></param>
    /// <param name="intensityX"></param>
    /// <param name="intensityY"></param>
    void HeadBob(float z, float intensityX, float intensityY)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z) * intensityX, Mathf.Sin(z * 2) * intensityY, 0);
    }
}
