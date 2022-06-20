using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float timer = 2;

    // Will destroy the gameobject when the timer reaches the set value in the inspector.
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
