using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask IsPlayer, IsGround;

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float sightRange, attackRange, aggroRange;
    public bool playerInSightRange, playerInAttackRange, playerInAggroRange, continueAggro;

    public float lastAction, pauseTime;
    public float chaseSpeed;
    public float roamSpeed;

    public bool hasAttacked;
    public float attackDelay;
    public BoxCollider attackBox;

    public Animator animator;
    
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attackBox = GetComponentInChildren<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
        pauseTime = pauseTime = Random.Range(1, 3);
        continueAggro = false;
    }

    private void Update()
    {
        if (Time.time < lastAction + pauseTime)
            return;
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, IsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, IsPlayer);
        playerInAggroRange = Physics.CheckSphere(transform.position, aggroRange, IsPlayer);
        
        if (playerInSightRange)
            continueAggro = true;
        if (!playerInAggroRange)
        {
            continueAggro = false;
            animator.SetBool("isChasing", false);
        }
        
        if (!playerInSightRange && !playerInAttackRange && !continueAggro) Roaming();
        if (continueAggro && !hasAttacked) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Roaming()
    {
        agent.speed = roamSpeed;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isWalking", true);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) 
        {
            walkPointSet = false;
            animator.SetBool("isWalking", false);
        }
        
        lastAction = Time.time;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, IsGround))
            walkPointSet = true;

    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        animator.SetBool("isWalking", false);
        animator.SetBool("isChasing", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!hasAttacked)
        {
            attackBox.enabled = true;
            animator.SetBool("isAttacking", true);
            
            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }

    private void ResetAttack()
    {
        attackBox.enabled = false;
        animator.SetBool("isAttacking", false);
        hasAttacked = false;
    }
}
