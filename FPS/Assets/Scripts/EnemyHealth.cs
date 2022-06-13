using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public GameObject EnemyDeadFX;
    public Transform deadPoint;

    /// <summary>
    /// Set current health of the enemy to max health
    /// Created by; NghiaDC (11/6/2022)
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// reduce enemy's health when ever it get hits
    /// Created by: NghiaDC (11/6/2022)
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// added Die Behavior for the enemy
    /// Created by: NghiaDC (11/6/2022)
    /// </summary>
    private void Die()
    {
        var deadFXclone = Instantiate(EnemyDeadFX, deadPoint.transform.position, Quaternion.Euler(0, 180, 0));
        Destroy(deadFXclone, 2.0f);
        Destroy(gameObject);
    }
}
