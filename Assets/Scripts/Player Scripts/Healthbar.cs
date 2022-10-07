using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
   
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed;
    public float target;

    public void UpdateHealthBar(float maxHealth, float currentHealth) 
    {
        target = currentHealth / maxHealth;
    }

    private void Update() {
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

}
