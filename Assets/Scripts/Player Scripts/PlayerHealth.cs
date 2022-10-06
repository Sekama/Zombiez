using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    
    private void Start() 
    {
        currentHealth = maxHealth;
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

        if (currentHealth <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
