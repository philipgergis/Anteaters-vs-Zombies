// Philip Gergis and Caleb

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// bullet goes through enemies and freezes them for a second
public class ElectricBulletBehavior : BulletBehavior
{
    [SerializeField]
    protected float debuffDuration; // how long the debuff lasts for

    // edited so that the bullet does not delete on impact with an enemy and goes through them instead
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
            return;
        }

        // Deal this damage to the enemy.
        targetHealth.TakeDamage(damage, bank);

        // apply the speed debuff with a timer
        StartCoroutine(ApplyParalyzeDebuff(targetMovement));
    }

    protected IEnumerator ApplyParalyzeDebuff(EnemyMovement enemyMovement)
    {

        // edits enemy movement speed
        float originalSpeed = enemyMovement.GetSpeed();
        enemyMovement.EditSpeed(0, false);

        // waits for debuff duration then returns speed to original speed
        yield return new WaitForSeconds(debuffDuration);
        enemyMovement.EditSpeed(originalSpeed, true);
    }

}
