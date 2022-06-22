using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    public GameObject hitBlast;
    public Transform hitArea;

    // Will spawn the hitBlast gameobject when the bullet hits the hit area's transform.
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitBlast, hitArea.transform.position, Quaternion.identity);

            Destroy(other.gameObject);
        }
    }
}
