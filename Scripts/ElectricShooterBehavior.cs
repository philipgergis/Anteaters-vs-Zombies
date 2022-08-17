// Philip Gergis and Caleb

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AOE effect is the player shoots faster
public class ElectricShooterBehavior : Shooter
{
    [SerializeField]
    protected float rateMultiplier = 0.5f; // rate turret shoots

    // when they are colliding, the debuff remains
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyRateEffect(rateMultiplier, collision); 
    }

    // when the collision is done, the debuff goes away
    private void OnTriggerExit2D(Collider2D collision)
    {
        ApplyRateEffect(1f / rateMultiplier, collision);
    }

    private void ApplyRateEffect(float effect, Collider2D collision)
    {
        // ... and find their rigidbody.
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();

        // If they don't have a rigidbody, end function.
        if (!targetRigidbody)
        {
            return;
        }

        // Find the EnemyHealth script associated with the rigidbody.
        PlayerControls targetMovement = targetRigidbody.GetComponent<PlayerControls>();

        // If there is no EnemyHealth script attached to the gameobject, go on to the next collider.
        if (!targetMovement)
        {
            return;
        }

        targetMovement.EditFireRate(effect); // edit the fire of the player 
    }
}
