// Philip, Amanda, Amber

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls enemy movement
public class EnemyMovement : MonoBehaviour
{
    // temporary position
    private Vector3 tempPos;

    // current speed of the enemy
    protected float speed;

    // temporary speed
    [SerializeField]
    private float orgSpeed;

    // boolean to decide which direction to send the zombies
    [SerializeField]
    private bool MoveRight;

    // moves the zombie in a left or right direction
    public virtual void FixedUpdate()
    {
        if (MoveRight)
        {
            transform.Translate(2 * Time.fixedDeltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.fixedDeltaTime * speed, 0, 0);
        }
    }

    // stops the enemy when it reaches the left side of the map
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("ZombieEndLine"))
        {
            speed = 0;
        }
    }

    private void Start()
    {
        speed = orgSpeed;
    }

    // save position before collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        tempPos = transform.position;
    }

    // keep enemy at position if they are colliding with a turret or player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Turret"))
        {
            //speed = 0;
            transform.position = tempPos;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    // return speed and remove any velocity applied
    private void OnCollisionExit2D(Collision2D collision)
    {
        //speed = orgSpeed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // edits the current speed on the amount taken for a certain amount of time
    // true value adds speed false multiplies it
    public void EditSpeed(float speedChange, bool addValue)
    {
        if (addValue)
        {
            speed += speedChange;
        }
        else
        {
            speed *= speedChange;
        }
    }

    // return speed value
    public float GetSpeed()
    {
        return speed;
    }
}
