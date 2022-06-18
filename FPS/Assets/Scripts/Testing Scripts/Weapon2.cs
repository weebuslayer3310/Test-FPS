using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon2 : MonoBehaviour
{
    public Gun[] loadout;
    public Transform weaponParent;

    private int currentIndex;
    private GameObject currentWeapon;

    private void Start()
    {
        
    }

    private void Update()
    {
        //equip function.
        InputEquip();

        //
        if(currentWeapon != null)
        {
            Aim(Input.GetMouseButton(1));
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
        Transform anchor = currentWeapon.transform.Find("Anchor");
        Transform ADS = currentWeapon.transform.Find("States/ADS");
        Transform Hip = currentWeapon.transform.Find("States/Hip");

        if (isAiming)
        {
            //aim
            anchor.position = Vector3.Lerp(anchor.position, ADS.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
        else
        {
            //hip
            anchor.position = Vector3.Lerp(anchor.position, Hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
    }
}
