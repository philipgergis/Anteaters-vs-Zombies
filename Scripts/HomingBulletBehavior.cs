// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bullet follows enemies close to it
public class HomingBulletBehavior : BulletBehavior
{
    [SerializeField]
    private Transform cord; // first point rectangle for vision

    [SerializeField]
    private Transform cord2;  // second point rectangle for vision

    protected override void FixedUpdate()
    {
        Transform closest = null; // closest object
        float minDistance = -1f;  // closest distnce
        Collider2D[] collisions = Physics2D.OverlapAreaAll(cord.position, cord2.position); // all objects in area

        // finds newest closest enemy
        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Enemy" && (Vector3.Distance(coll.GetComponent<Transform>().position, transform.position) < minDistance || minDistance == -1))
            {
                closest = coll.GetComponent<Transform>();
                minDistance = Vector3.Distance(closest.position, transform.position);
            }
        }

        // if there is a enemy in vision, chase them, if not keep going forward
        if (closest)
        {
            transform.position = Vector2.MoveTowards(transform.position, closest.position, 1.5f * speed * Time.deltaTime);
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
