// Philip Gergis and Caleb

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bullet shrinks over time but starts out larger
public class EarthBulletBheavior : BulletBehavior
{
    [SerializeField]
    protected float scaleDown; // rate at which bullet shrinks

    [SerializeField]
    protected float minMagnitude; // the minimum size of the bullet before it is deleted

    protected void Update()
    {
        // the bullet shrinks and once it reaches a value less than the minimum, it is deleted
        gameObject.transform.localScale *= scaleDown;
        if(gameObject.transform.localScale.magnitude < minMagnitude)
        {
            Destroy(gameObject);
        }
    }
}
