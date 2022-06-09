using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponHolder;
    public GameObject pistolWeapon;
    public GunSystem gunSystem;

    void Update()
    {
        Aim(Input.GetMouseButton(1));

        //weapon position elasticity
        gunSystem.currentWeapon.transform.localPosition = Vector3.Lerp(gunSystem.currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
    }

    private void Aim(bool isAiming)
    {
        Transform weaponAnchor = pistolWeapon.transform.Find("Anchor");
        Transform state_ads = pistolWeapon.transform.Find("States/ADS");
        Transform state_hip = pistolWeapon.transform.Find("States/Hip");

        if (isAiming)
        {
            //aim
            weaponAnchor.position = Vector3.Lerp(weaponAnchor.position, state_ads.position, Time.deltaTime * gunSystem.aimSpeed);
            gunSystem.spread = 0.03f;
            gunSystem.kickback = 0.005f;
        }
        else
        {
            //hip
            weaponAnchor.position = Vector3.Lerp(weaponAnchor.position, state_hip.position, Time.deltaTime * gunSystem.aimSpeed);
            gunSystem.spread = 0.2f;
            gunSystem.kickback = 0.1f;
        }
    }
}
