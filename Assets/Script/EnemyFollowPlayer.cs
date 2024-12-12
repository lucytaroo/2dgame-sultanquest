using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    private Transform player;
    public Animator animator;

    private bool IsDead = false;

    public int attackDamage = 20;
    
    
    public Transform attackPoint;
    public LayerMask playerLayer;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool hasAttacked = false;
    void Update()
    {

        if (player == null)
        {
            animator.SetBool("Attack", false);
            return;  // Player is null, avoid further processing
        }
            
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (distanceFromPlayer < attackRange)   
            {
                // Attack player
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);

                if (!hasAttacked)
                {
                    Attack();
                    hasAttacked = true;  // Set the flag to true after attacking
                }
            
            }
            else if (distanceFromPlayer < lineOfSight)
            {
                if(!IsDead)
                {
                    // Follow player
                    animator.SetBool("Attack", false);
                    animator.SetBool("Walk", true);

                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    if (transform.position.x > player.position.x)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (transform.position.x < player.position.x)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                else
                {
                    // Idle state
                    animator.SetBool("Attack", false);
                    animator.SetBool("Walk", false);
                }
            }
            else
            {
                // Idle state
                animator.SetBool("Attack", false);
                animator.SetBool("Walk", false);
                hasAttacked = false;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        // Draw line of sight
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        

       
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
