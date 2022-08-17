// Philip Gergis and Caleb

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// slows the player down when in its vicinity
public class EarthShooterBehavior : Shooter
{
    [SerializeField]
    private float rateMultiplier = 2f; // shooting debuff ratio

    [SerializeField]
    private float speedDebuff = 0.75f; // speed debuff ratio

    // applies debuff on trigger enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDebuffEffect(rateMultiplier, speedDebuff, collision);
    }

    // ends debuff on trigger exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        ApplyDebuffEffect(1f / rateMultiplier, 1f/ speedDebuff, collision);
    }

    private void ApplyDebuffEffect(float rateEffect, float speedEffect, Collider2D collision)
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

        targetMovement.EditFireRate(rateEffect); // slows player shooting rate
        targetMovement.EditSpeed(speedEffect); // slows player's speed
    }
}
