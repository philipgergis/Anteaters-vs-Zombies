// Amanda and Philip Gergis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AI enemy that will dodge projectiles fired from the player 
public class EnemyDodge : EnemyMovement
{
    [SerializeField]
    private float speedUp; // speed up of enemy 

    [SerializeField]
    private Transform cord; // first point rectangle for vision

    [SerializeField]
    private Transform cord2;  // second point rectangle for vision

    [SerializeField]
    private Transform above1;  // first point for vision above

    [SerializeField]
    private Transform above2;  // second point for vision above

    [SerializeField]
    private Transform below1;  // first point for vision below

    [SerializeField]
    private Transform below2;  // second point for vision below

    [SerializeField]
    private float magnitudeY; // highest/lowest y coordinate enemy can be at


    private void Update()
    {
        // updates direction it dodges based on where it is on the map
        if (transform.position.y + 1f >= magnitudeY)
        {
            speedUp = -Mathf.Abs(speedUp);
        }
        if (transform.position.y - 1f <= -magnitudeY)
        {
            speedUp = Mathf.Abs(speedUp);
        }
    }


    public override void FixedUpdate()
    {
        Collider2D collision = Physics2D.OverlapArea(cord.position, cord2.position, LayerMask.GetMask("Bullets")); // hazards in the way

        // if there is a bullet in proximity, move to avoid, if not then continue forward 
        if (collision)
        {
            DodgeMovement();
        }
        else
        {
            base.FixedUpdate();
        }
    }
    
    // dodges based on if the enemy can go up or not
    private void DodgeMovement()
    {
        // checks for objects in a path above and below
        Collider2D collisionUp = Physics2D.OverlapArea(above1.position, above2.position, LayerMask.GetMask("Turrets",  "Enemies")); 
        Collider2D collisionDown = Physics2D.OverlapArea(below1.position, below2.position, LayerMask.GetMask("Turrets", "Enemies"));
        
        // dodges if nothing is blocking its way that it collides with
        if ((speedUp > 0 && collisionUp == null) || (speedUp < 0 && collisionDown == null))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1f), speed * speedUp * Time.fixedDeltaTime);
        }

        // dodges opposite way if original way is blocked
        else if(speedUp > 0 && collisionDown == null && collisionUp || speedUp < 0 && collisionUp == null && collisionDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1f), speed * -speedUp * Time.fixedDeltaTime);
        }

        // move forward if cant dodge
        else
        {
            transform.Translate(2 * Time.fixedDeltaTime * -speed, 0, 0);
        }
    }
}
