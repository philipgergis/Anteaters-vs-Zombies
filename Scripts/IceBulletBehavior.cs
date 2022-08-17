// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// applies slow effect on enemies when hit
public class IceBulletBehavior : BulletBehavior
{
    [SerializeField]
    protected float speedDebuffMultiplier = 0.5f; // debuff amount on speed

    [SerializeField]
    protected float debuffDuration = 2f; // how long the debuff lasts for

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

        // Find the EnemyHealth script associated with the rigidbody.
        EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();
        EnemyMovement targetMovement = targetRigidbody.GetComponent<EnemyMovement>();

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetHealth || !targetMovement)
        {
            Destroy(gameObject);
            return;
        }

        // Deal this damage to the enemy.
        targetHealth.TakeDamage(damage, bank);

        // disable bullet 
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // apply the speed debuff with a timer
        StartCoroutine(ApplySpeedDebuff(targetMovement));
    }

    protected IEnumerator ApplySpeedDebuff(EnemyMovement targetMovement)
    {
        // Apply slow effect
        targetMovement.EditSpeed(speedDebuffMultiplier, false);

        // have duration last for as long as debuff duration
        yield return new WaitForSeconds(debuffDuration);

        // reverse slow effect
        targetMovement.EditSpeed(1f / speedDebuffMultiplier, false);

        // Destroy object on impact
        Destroy(gameObject);
    }
}
