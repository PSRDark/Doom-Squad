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
    //0 = left, 1 = right;
    public int direction = 1;

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
        if (direction == 1)
        {
            Flip();
            direction = 0;
        }
        else direction = 0;

        if (isGrounded)
        playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
        else playerRigidbody.velocity = new Vector2(-speed * 0.7f, playerRigidbody.velocity.y);
    }

    public void MoveRight()
    {
        if (direction == 0)
        {
            Flip();
            direction = 1;
        }
        else direction = 1;

        if (isGrounded)
        playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
        else playerRigidbody.velocity = new Vector2(speed * 0.7f, playerRigidbody.velocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
    }

    public void Flip()
    {
        gameObject.GetComponent<Shooting>().direction *= -1;
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
    public Collision2D Other;
    void OnCollisionEnter2D(Collision2D other)
    {
        Other = other;
        if(other.transform.tag == "Trench")
        {
            if ((other.transform.rotation.y == 0 && transform.position.y < other.transform.position.y && transform.position.x < other.transform.position.x) || (other.transform.rotation.y != 0 && transform.position.y < other.transform.position.y && transform.position.x > other.transform.position.x))
            {
                other.transform.gameObject.layer = 10;
            }
        }

        if(other.transform.tag == "FallingGround")
        {
            StartCoroutine(ActivateGravity(0.3f));
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Trench")
        {
            if ((other.transform.rotation.y == 0 && transform.position.y < other.transform.position.y && transform.position.x < other.transform.position.x) || (other.transform.rotation.y != 0 && transform.position.y < other.transform.position.y && transform.position.x > other.transform.position.x))
            {
                other.transform.gameObject.layer = 8;
            }
        }
    }

    IEnumerator ActivateGravity(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            Other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
