// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the general behavior of a bullet, including movement and behavior
public class BulletBehavior : MonoBehaviour
{
    // Speed for bullet, must be faster than speed of player
    [SerializeField]
    protected float speed = 10.0f;

    // damage of the bullet
    [SerializeField]
    protected float damage = 1f;

    // rigid body of the bullet
    protected Rigidbody2D bulletBody;

    // bank bullet gives money to when killing enemies
    protected MoneyManager bank;

    // retrieves the rigid body on the bullet
    protected void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        // Keeps moving in the direction it was spawned
        bulletBody.velocity = transform.right * speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        bulletCollision(collision);
    }

    protected virtual void bulletCollision(Collider2D collision)
    {
        // ... and find their rigidbody.
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();

        // If they don't have a rigidbody, end function.
        if (!targetRigidbody)
        {
            return;
        }

        // check if object is a bullet deleter
        BulletDeleter deleter = targetRigidbody.GetComponent<BulletDeleter>();

        // if bullet deleter do not delete bullet and end
        if(deleter)
        {
            return;
        }

        // Find the EnemyHealth script associated with the rigidbody.
        EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetHealth)
        {
            Destroy(gameObject);
            return;
        }

        // Deal this damage to the enemy.
        targetHealth.TakeDamage(damage, bank);

        // Destroy object on impact
        Destroy(gameObject);
    }

    public void SetBank(MoneyManager newBank)
    {
        bank = newBank;
    }
}
