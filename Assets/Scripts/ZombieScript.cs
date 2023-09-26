using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private Transform playerTransform;
    private GameObject playerObject;
    public float runningSpeed = 5f;
    public float detectionDistance = 5f;
    public float attackDistance = 2f;
    public int damageAmount;
    private float attackTime = 0.5f;
    private float currentAttackTime = 0f;
    private bool isRunning = false;
    private bool isAttacking = false;
    private float distanceToPlayer;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionDistance && distanceToPlayer > 1f)
        {
            isRunning = true; 
        }
        else
        {
            isRunning = false;
        }

        if (isRunning && !isAttacking)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.position += directionToPlayer * runningSpeed * Time.deltaTime;
        }

        if (distanceToPlayer <= attackDistance && !isAttacking)
        {
            isAttacking = true;
        }

        if (isAttacking)
        {
            AttackPlayer();
        }
    }


    private void AttackPlayer()
    {
        currentAttackTime += Time.deltaTime;
            
        if (currentAttackTime >= attackTime)
        {
            if (distanceToPlayer <= attackDistance)
            {
                var pHealth = playerObject.GetComponent<PlayerHealth>();
                pHealth.TakeDamage(damageAmount);
            }
            isAttacking = false;
            currentAttackTime = 0f;
        }
    }
}
