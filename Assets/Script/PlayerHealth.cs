using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    bool IsDead = false;
    private Transform player;
    public HealthBar healthBar;
    public Canvas gameOverCanvas;
    public float delayTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            // Initialize the health bar with the player's maximum health
            healthBar.SetMaxHealth(maxHealth);
            
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        Debug.Log("player Take damage");
        

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
            
            Debug.Log("Game Over");
        }

    }


    void Die()
    {
        if (IsDead) return;
        Debug.Log("Player Died!");
        animator.SetBool("IsDead", true);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;

        }
        IsDead = true;

        
        this.enabled = false;
        Destroy(gameObject, 2.0f);
    }

    private void ActivateGameOverCanvas()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
        }
        
    }
}
