using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Healthbar healthbar;
    
    private void Start() 
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "AttackBox")
        {
            TakeDamage(1);
        }
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0 )
        {
            gameOverScreen.SetActive(true);

        }
    }
}
