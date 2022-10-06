using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ZombieHealth : MonoBehaviour
{
     public float health;
     public Animator animator;
     public EnemyAI enemyAI;
     public CapsuleCollider capsuleCollider;

    private void Start() 
    {
        health = Random.Range(1,3);
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        enemyAI.enabled = false;
        capsuleCollider.enabled = false;
        animator.SetBool("isDying", true);
        Destroy(gameObject, 6);
    }
}
