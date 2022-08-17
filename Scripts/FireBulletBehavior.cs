// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the bullet deals a DOT effect to enemies hit
public class FireBulletBehavior : BulletBehavior
{
    [SerializeField]
    protected float debuffInterval; // time debuff lasts

    [SerializeField]
    protected int debuffTimes; // how many times the debuff is applied

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

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetHealth)
        {
            Destroy(gameObject);
            return;
        }

        // disable bullet 
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // apply the speed debuff with a timer
        StartCoroutine(ApplyFireDamage(targetHealth));
    }

    // applies damage over time
    protected IEnumerator ApplyFireDamage(EnemyHealth targetHealth)
    {
        int i = 0;
        while(targetHealth && i < debuffTimes)
        {
            targetHealth.TakeDamage(damage, bank);
            yield return new WaitForSeconds(debuffInterval);
            i++;
        }
        Destroy(gameObject);
    }

}
