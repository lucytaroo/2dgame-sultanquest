using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;
namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;
        private bool facingRight = false;
        private bool isGrounded;
        public Transform groundCheck;
        private Rigidbody2D rb;
        private Animator animator;

        public GameObject jumpDust;
        [SerializeField] private AudioSource JumpSFX;
        
        //TESTESTTEST
        private Transform player;
        public int hitDamage = 20;

        private int life = 3;
        private float hurtForce = 8f;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = transform;
        }
        private void FixedUpdate()
        {
            CheckGround();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position +
                direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation
                
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); //Turn on idle animation
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                JumpSFX.Play();
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                StartCoroutine(PlayJumpDust()); // Play the jump dust effect
            }
            if (!isGrounded) animator.SetInteger("playerState", 2); //Turn on jump animation
            if (facingRight == false && moveInput < 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput > 0)
            {
                Flip();
            }
        }
        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        
        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        IEnumerator PlayJumpDust()
        {
            Vector3 jumpStartPosition = transform.position;

            // Add an offset to the y-coordinate to make it higher
            float yOffset = 0.5f; // Adjust the value based on your preference
            jumpDust.transform.position = new Vector3(jumpStartPosition.x, jumpStartPosition.y + yOffset, jumpStartPosition.z);

            jumpDust.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            jumpDust.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "WeakEnemy")
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    //life -= 1;
                    Debug.Log("player hit collide");
                    playerHealth.TakeDamage(10);
                }
                else
                {
                    Debug.Log("code errorrororor");
                }

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

            }
        }
    }
}
