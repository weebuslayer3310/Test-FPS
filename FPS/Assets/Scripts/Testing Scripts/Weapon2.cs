using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon2 : MonoBehaviour
{
    public Gun[] loadout;
    public Transform weaponParent;

    private int currentIndex;
    private GameObject currentWeapon;
    public bool Aiming = false;

    public GameObject bulletHolePrefabs;
    public LayerMask canBeShot;
    private float currentCooldown;

    private void Update()
    {
        //equip function.
        InputEquip();

        //
        if(currentWeapon != null)
        {
            Aim(Input.GetMouseButton(1));

            if(loadout[currentIndex].burst != 1)
            {
                if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && currentCooldown <= 0)
                {
                    Shoot();
                }
            }

            //weapon position elasticity
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4.0f);
        
            //cooldown
            if(currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// change weapons with numerical buttons.
    /// Created by: NghiaDc (18/6/2022)
    /// </summary>
    void InputEquip()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(2);
        }
    }

    /// <summary>
    /// Equip difference kind of weapon with scriptable objects.
    /// </summary>
    /// <param name="weaponLoadout"></param>
    void Equip(int weaponLoadout)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentIndex = weaponLoadout;

        GameObject newWeapon = Instantiate(loadout[weaponLoadout].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = newWeapon;
    }

    void Aim(bool isAiming)
    {
        Aiming = isAiming;
        Transform anchor = currentWeapon.transform.Find("Anchor");
        Transform ADS = currentWeapon.transform.Find("States/ADS");
        Transform Hip = currentWeapon.transform.Find("States/Hip");

        if (isAiming)
        {
            //aim
            anchor.position = Vector3.Lerp(anchor.position, ADS.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            loadout[currentIndex].spread = 20.0f;
            loadout[currentIndex].kickBack = 0.05f;
        }
        else
        {
            //hip
            anchor.position = Vector3.Lerp(anchor.position, Hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            loadout[currentIndex].spread = 60.0f;
            loadout[currentIndex].kickBack = 0.3f;
        }
    }

    /// <summary>
    /// shooting for the player.
    /// Created by: NghiaDC (20/6/2022)
    /// </summary>
    private void Shoot()
    {
        //cooldown
        currentCooldown = loadout[currentIndex].fireRate;

        Transform spawn = transform.Find("Main Camera/WeaponCamera");
        
        for(int i = 0; i < Mathf.Max(1, loadout[currentIndex].pellets); i++)
        {
            //spread
            Vector3 bloom = spawn.position + spawn.forward * 1000f;
            bloom += Random.Range(-loadout[currentIndex].spread, loadout[currentIndex].spread) * spawn.up;
            bloom += Random.Range(-loadout[currentIndex].spread, loadout[currentIndex].spread) * spawn.right;
            bloom -= spawn.position;
            bloom.Normalize();

            //Raycast
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(spawn.position, bloom, out hit, 100f, canBeShot))
            {
                var BulletHole = Instantiate(bulletHolePrefabs, hit.point + hit.normal * 0.001f, Quaternion.identity);
                BulletHole.transform.LookAt(hit.point + hit.normal);
                Destroy(BulletHole, 2.0f);

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(loadout[currentIndex].damage);
                }

            }
        }
        

        //gun recoil.
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickBack;

    }
}
