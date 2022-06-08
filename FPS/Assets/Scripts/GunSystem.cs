using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;
    public int mangazineSize;
    public int bulletsPerTap;
    public bool allowButtonHold;
    private int bulletsLeft;
    private int bulletsShot;

    [Header("Gun bools")]
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;

    [Header("Gun's Reference")]
    private Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Gun's Graphic")]
    public GameObject muzzleFlash;
    public GameObject bulletHoleGraphic;
    private TextMeshProUGUI text;
    public float aimSpeed;

    private void Awake()
    {
        if(fpsCam == null)
        {
            fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        }
        text = GameObject.FindGameObjectWithTag("Ammo").GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
        bulletsLeft = mangazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + mangazineSize);
    }

    private void MyInput()
    {
        //get input from the left-mouse-button.
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if(!allowButtonHold)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //callout reload funtion.
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < mangazineSize && !reloading)
        {
            Reload();
        }

        //shoot.
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    /// <summary>
    /// Function for shooting our weapons
    /// Created by: NghiaDC (6/6/2022)
    /// </summary>
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with Spread.
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //Raycast.
        if(Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            //if (rayHit.collider.CompareTag("Enemy"))
            //{
                  //rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
            //    Debug.Log("we hit an enemy!!");
            //}
        }

        //Graphics.
        var bulletHoleClone = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Destroy(bulletHoleClone, 2.0f);
        var muzzleFlashClone = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(muzzleFlashClone, 2.0f);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    /// <summary>
    /// Function for re-loading our weapons
    /// Created by: NghiaDC (6/6/2022)
    /// </summary>
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = mangazineSize;
        reloading = false;
    }
}
