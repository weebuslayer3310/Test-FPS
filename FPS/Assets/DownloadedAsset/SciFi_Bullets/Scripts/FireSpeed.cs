using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeed : MonoBehaviour
{

    public float fireSpeed = 5f;


    // Will push the game object forward.
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * fireSpeed;
    }
}
