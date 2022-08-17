// Amanda and Philip and Alex


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// AI enemy, when within vicinity of player, will chase player at an increased speed
// when outside the vicinity of the player, the enemy will continue towards the endline 
public class EnemyDefend : EnemyMovement
{
    [SerializeField]
    private float speedUp = 0.7f; // speed up when chasing

    [SerializeField]
    private Transform cord; // first point rectangle for vision

    [SerializeField]
    private Transform cord2;  // second point rectangle for vision

    public override void FixedUpdate()
    {
        Transform closest = null; // closest object
        float minDistance = -1f;  // closest distnce
        Collider2D[] collisions = Physics2D.OverlapAreaAll(cord.position, cord2.position); // all objects in area

        // finds newest closest player
        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Enemy" && (Vector3.Distance(coll.GetComponent<Transform>().position, transform.position) < minDistance || minDistance == -1))
            {
                closest = coll.GetComponent<Transform>();
                minDistance = Vector3.Distance(closest.position, transform.position);
            }
        }

        // if there is a player in vision, chase them, if not keep going forward
        if (closest)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, closest.position.y), speed * speedUp * Time.fixedDeltaTime);
        }
        base.FixedUpdate();

    }
}