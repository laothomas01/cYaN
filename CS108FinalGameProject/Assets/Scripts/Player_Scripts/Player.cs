using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    //public HealthBar healthBar;
    Animator animator;

    [Header("iFrames")]
    //[SerializeField] public float iFramesDuration;
    private bool isInvincible = false;
    //[SerializeField] public int numberOfFlashes;
    [SerializeField] private float iFramesDuration;
    //public SpriteRenderer spriteRend;

    //public Collider2D collision;

    void Awake()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        //spriteRend = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

    }

    // take damage when colliding on objects with "Enemies"
    // tag
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemy hits player");
            if (isInvincible) return;
            TakeDamage(2);
        }
        //else if (collision.gameObject.CompareTag("void"))
        //{
        //    TakeDamage(100);
        //}
        //else if (collision.gameObject.CompareTag("trap"))
        //{
        //    TakeDamage(10);
        //    if (isInvincible) return;
        //}
    }

    void TakeDamage(int damage)
    {

        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        //healthBar.SetHealth(currentHealth);

        if (!isInvincible)
        {
            StartCoroutine(Invunerability());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Character died!");

        /** Die animation */
        //animator.SetBool("IsDead", true);



        animator.SetTrigger("PlayerDead");
        GetComponent<PlayerMovement>().enabled = false;
        this.GetComponentInChildren<Weapon>().enabled = false;



        GetComponent<PlayerMovement>().enabled = false;

        //when player dies, instantiate a player death animation object. 

        /** Disable the enemy*/
        //GetComponent<BoxCollider2D>().enabled = false;                 // removes the collider of enemy
        //GetComponent<CircleCollider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;             // removes the rendered sprite
        //this.enabled = false;                                       // disables this script

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Invunerability()
    {

        Debug.Log("Player turned invincible!");
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //spriteRend.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        //spriteRend.color = Color.white;


        isInvincible = false;
        Debug.Log("Player no longer invincible!");

    }




}
