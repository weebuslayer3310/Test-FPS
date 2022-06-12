using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public HealthUI healthUI;

    /// <summary>
    /// Set current health of the player to max health
    /// Created by; NghiaDC (11/6/2022)
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
    }

    /// <summary>
    /// reduce player's health when ever it get hits
    /// Created by: NghiaDC (11/6/2022)
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthUI.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("You're Dead!!!");
    }
}
