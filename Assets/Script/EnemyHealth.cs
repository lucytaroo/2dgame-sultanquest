using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth<=0)
        {

            Die();
        }

    }

    void Die()
    {
        if (IsDead) return;
        Debug.Log("Enemy Died!");
        animator.SetBool("IsDead",true);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            
        }
        IsDead = true;
        this.enabled = false;
        Destroy(gameObject, 2.0f);
    }
}
