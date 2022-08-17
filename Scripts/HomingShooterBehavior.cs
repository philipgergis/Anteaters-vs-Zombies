// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AOE effect is the player cannot shoot, and the turret shoots two bullets from different spots
public class HomingShooterBehavior : Shooter
{

    [SerializeField]
    protected Transform ShooterFirePoint2; // where shooter shoots from other point

    // when they are colliding, the debuff remains
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyRateEffect(false, collision);
    }

    // when the collision is done, the debuff goes away
    private void OnTriggerExit2D(Collider2D collision)
    {
        ApplyRateEffect(true, collision);
    }

    private void ApplyRateEffect(bool shoot, Collider2D collision)
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

        targetMovement.DetermineShoot(shoot); // edit the fire of the player 
    }

    protected override void Update()
    {
        // keeps subtracting timebtwshots until it is less than 0, then shoots 2 shots again
        if (timeBtwShots <= 0)
        {
            SpawnBullet(ShooterFirePoint.position, Quaternion.identity);
            SpawnBullet(ShooterFirePoint2.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
