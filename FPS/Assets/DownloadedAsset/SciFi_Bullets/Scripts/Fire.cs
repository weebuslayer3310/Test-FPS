using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletParticleLine;
    public GameObject blastParticleLine;
    public Transform gunBulletObject;

    public float shotDelay = .5f;

    float timer;


    // Will fire when F is pressed and the timer reaches the value for shotDelay.
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && timer >= shotDelay)
        {
            FireBullet();
        }
    }

    // Will set the timer back to 0, allow the gun's animation to play, spawn the particle effects to the empty gameobject's transform, and after the clip's animation is played will set the animator to false.
    public void FireBullet()
    {
        timer = 0;

       
        Instantiate(bulletParticleLine, gunBulletObject.transform.position, Quaternion.identity);
        Instantiate(blastParticleLine, gunBulletObject.transform.position, Quaternion.identity);

    }
}
