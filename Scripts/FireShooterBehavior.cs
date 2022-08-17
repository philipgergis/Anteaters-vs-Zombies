// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// applies damage over time when around turret
public class FireShooterBehavior : Shooter
{

    [SerializeField]
    private float damage = 0.5f; // damage of debuff


    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        // ... and find their rigidbody.
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();

        // If t hey don't have a rigidbody, end function.
        if (!targetRigidbody)
        {
            return;
        }

        // Find the EnemyHealth script associated with the rigidbody.
        EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetHealth)
        {
            return;
        }

        targetHealth.TakeDamage(damage, bank); // deal damage to enemy
    }
}
