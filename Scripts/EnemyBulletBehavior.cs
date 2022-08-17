using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : BulletBehavior
{
    protected override void FixedUpdate()
    {
        // Keeps moving in the direction it was spawned
        bulletBody.velocity = transform.right * -speed;
    }

    protected override void bulletCollision(Collider2D collision)
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
        if (deleter)
        {
            return;
        }

        // If the object is a player or shooter, deal damage
        if (targetRigidbody.tag == "Player")
        {
            PlayerHealth playerHealth = targetRigidbody.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
        if (targetRigidbody.tag == "Turret")
        {
            Shooter shooterHealth = targetRigidbody.GetComponent<Shooter>();
            shooterHealth.TakeDamage(damage);
        }

        // Destroy object on impact
        Destroy(gameObject);
    }
}
