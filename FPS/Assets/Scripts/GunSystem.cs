using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public GameObject currentWeapon;

    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting;
    public float spread;
    public float recoil;
    public float kickback;
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
    public Transform shellPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Gun's Graphic")]
    public GameObject muzzleFlash;
    public GameObject bulletHoleGraphic;
    public GameObject shells;
    private TextMeshProUGUI text;
    public float aimSpeed;

    [Header("Gun Sound")]
    public AudioClip[] GunShotSounds;
    private AudioSource audioSource;
    public AudioClip ReloadSound;
    public AudioClip[] ImpactSound;

    private void Awake()
    {
        if(fpsCam == null)
        {
            fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        }
        text = GameObject.FindGameObjectWithTag("Ammo").GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
        bulletsLeft = mangazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.Log("No Error Found!!");
        }
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

        Vector3 temporarySpread = fpsCam.transform.position + direction * 100f;
        temporarySpread += x * direction;
        temporarySpread += y * direction;

        temporarySpread -= fpsCam.transform.position;
        temporarySpread.Normalize();

        //Raycast.
        if (Physics.Raycast(fpsCam.transform.position, temporarySpread, out rayHit, range, whatIsEnemy))
        {
            if (rayHit.collider.CompareTag("Enemy"))
            {
                for (int i = 0; i < ImpactSound.Length; i++)
                {
                    audioSource.clip = ImpactSound[i]; 
                    audioSource.PlayOneShot(audioSource.clip);
                }
                
                rayHit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }

        //gun fx.
        currentWeapon.transform.Rotate(-recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * kickback;

        //shooting sfx.
        PlayShootingSound();

        //Graphics.
        var bulletHoleClone = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Destroy(bulletHoleClone, 2.0f);
        var muzzleFlashClone = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(muzzleFlashClone, 2.0f);
        var ShellsClone = Instantiate(shells, shellPoint.position, Quaternion.identity);
        Destroy(ShellsClone, 0.5f);


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
        audioSource.clip = ReloadSound;
        audioSource.PlayOneShot(audioSource.clip);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = mangazineSize;
        reloading = false;
    }

    private void PlayShootingSound()
    {
        //get an Audio Clip
        int n = Random.Range(1, GunShotSounds.Length);
        audioSource.clip = GunShotSounds[n];

        //Play the sound once
        audioSource.PlayOneShot(audioSource.clip);
    }
}
