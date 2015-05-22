using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour, IDamageable {

    //Stats
    public int health { get; private set; }
    public int shield { get; private set; }

    //Physics and movement
    [HideInInspector]public Rigidbody2D playerRigidbody;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundChecker;
    public float groundRadius;
    public LayerMask groundMask;

    void Start()
    {
        health = 100;
        shield = 50;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundRadius, groundMask);
    }

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveLeft()
    {
        if(isGrounded)
        playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
        else playerRigidbody.velocity = new Vector2(-speed * 0.7f, playerRigidbody.velocity.y);
    }

    public void MoveRight()
    {
        if(isGrounded)
        playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
        else playerRigidbody.velocity = new Vector2(speed * 0.7f, playerRigidbody.velocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
    }

    void OnTrench()
    {
        Debug.Log("On trench");
    }

    public void TakeDamage(int damage)
    {
        if(shield > 0)
        {
            if(shield >= damage)
            {
                shield -= damage;
            }
            else
            {
                health -= damage - shield;
                shield = 0;
            }
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
            Die();
    }
    public void Die()
    {
        Debug.Log("i died");
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.transform.tag == "Trench")
        {
            if ((other.transform.rotation.y == 0 && transform.position.y < other.transform.position.y && transform.position.x < other.transform.position.x) || (other.transform.rotation.y != 0 && transform.position.y < other.transform.position.y && transform.position.x > other.transform.position.x))
                OnTrench();
        }
    }
}
