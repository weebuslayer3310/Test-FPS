using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    private float speed = 30.0f;

    public GameObject bloodFX;

    public PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(20);

            Destroy(gameObject);
            var bloodClone = Instantiate(bloodFX, transform.position, Quaternion.identity);
            Destroy(bloodClone, 2.0f);
        }
    }
}
