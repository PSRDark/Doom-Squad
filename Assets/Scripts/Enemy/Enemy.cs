using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour, IDamageable {

    public int health;
    protected GameObject Player;
    public float distanceToSearch;
    public bool playerFound;
    protected Rigidbody2D Erigidbody;
    protected Vector3 direction = Vector3.left;
    public LayerMask layersRaycast;
    public float speed;
    public float distanceToFlip;
    public float jumpForce;
    public int damage;

    public virtual void Die()
    {
        Debug.Log("Enemy died");
    }

    protected virtual void Flip()
    {
        direction *= -1;
    }

    public virtual void TakeDamage(int Damage)
    {
        health -= Damage;
        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Damage(GameObject gameObjectToDamage)
    {
        IChecker.IDamageable(gameObjectToDamage).TakeDamage(damage);
    }

    protected virtual void Damage(Collider2D gameObjectToDamage)
    {
        IChecker.IDamageable(gameObjectToDamage).TakeDamage(damage);
    }

    public virtual void Patrol()
    {
        StartCoroutine(patrol());
    }

    protected virtual IEnumerator patrol()
    {
        yield return null;
    }

    public virtual void Hunting()
    {
        playerFound = true;
        StartCoroutine(hunting());
    }

    protected virtual IEnumerator hunting()
    {
        yield return null;
    }

    protected virtual void Start ()
    {
        health = 100;
        Player = GameObject.Find("Player");
        Erigidbody = GetComponent<Rigidbody2D>();
        Patrol();
	}

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Damage(other.collider);
    }

    protected virtual void Update()
    {
        if (playerFound)
        {
            Hunting();
        } else Patrol();

        if (Mathf.Abs(Player.transform.position.x - transform.position.x) < distanceToSearch)
        {
            Hunting();
        }
        else
        {
            playerFound = false;
            Patrol();
        }
    }
}
