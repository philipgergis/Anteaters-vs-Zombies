// Philip Gergis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// applies a slow aoe to enemies near it
public class IceShooterBehavior : Shooter
{
    [SerializeField]
    protected float speedDebuffMultiplier = 0.5f; // amount speed is divided by

    // applies debuff while in AOE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplySpeedEffect(speedDebuffMultiplier, collision);
    }

    // reverses debuff when leaving AOE
    private void OnTriggerExit2D(Collider2D collision)
    {
        ApplySpeedEffect(1f / speedDebuffMultiplier, collision);
    }
    

    private void ApplySpeedEffect(float effect, Collider2D collision)
    {
        // ... and find their rigidbody.
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();

        // If they don't have a rigidbody, end function.
        if (!targetRigidbody)
        {
            return;
        }

        // Find the EnemyHealth script associated with the rigidbody.
        EnemyMovement targetMovement = targetRigidbody.GetComponent<EnemyMovement>();

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetMovement)
        {
            return;
        }

        targetMovement.EditSpeed(effect, false); // edits speed of enemies
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
}
