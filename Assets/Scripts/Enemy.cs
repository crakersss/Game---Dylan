using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    private bool isAlive = true;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    public AttackZone attackZone;
    public EnemyAttack enemyAttack;
    Rigidbody2D rb;
    public float maxHealth = 1f; // Define maxHealth as a private field
    private float currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize currentHealth to maxHealth
    }

    public void StartEnemyAttack()
    {
        enemyAttack.StartAttack();
    }

    public void StopEnemyAttack()
    {
        enemyAttack.StopAttack();
    }

void FixedUpdate()
{
    if (isAlive == true){
    
        if (detectionZone.detectedObjects.Count > 0)
    {
        // Move object towards player
        Vector2 direction = (detectionZone.detectedObjects[0].transform.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed * Time.deltaTime);

        // Set isMoving to true when the enemy starts moving
        if (attackZone.detectedObjects.Count > 0)
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
        }

        // Flip the enemy sprite if moving left
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1f); // Flip along the X-axis
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1f); // Restore original scale
        }
    }
}
    }

    // Define a method to handle taking damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Check if the enemy is still alive
        if (currentHealth > 0)
        {
            // Trigger the animation for taking damage
            animator.SetTrigger("Take Damage");
        }
        else
        {
            // If the enemy's health drops to or below 0, call the Defeated method
            Defeated();
        }
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        isAlive = false;

    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    // Define the Health property to access currentHealth
    public float Health
    {
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Defeated();
            }
            else if (currentHealth < 0)
            {
                TakeDamage(0);
            }
        }
        get
        {
            return currentHealth;
        }
    }

        private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Check if the enemy is at a specific frame of the attack animation
            if (enemyAttack.IsAtSpecificFrame())
            {
                // Deal damage to player
                PlayerController player = collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(enemyAttack.damage); // set damage to the value set in the attack script
                    // Optionally trigger other effects or animations
                }
            }
        }
    }
}

