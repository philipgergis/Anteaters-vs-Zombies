// Philip Gergis and Alex

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ai script for handling movement and most controls
public class AIControls : PlayerControls
{
    // grid to find locations of turrets
    [SerializeField] private Grid grid;

    // points for the rectangle where ai can see zombies
    [SerializeField] private Transform zombies1;
    [SerializeField] private Transform zombies2;

    // points for the rectangle where ai can place turrets
    [SerializeField] private Transform turrets1;
    [SerializeField] private Transform turrets2;

    // bare minimum turrets to have on field
    [SerializeField] private int minTurrets = 6;

    // get ai turret control script
    private TurretControlsAI turretController;




    // shoots bullets at any moment it can
    protected override void ShootBullet()
    {
        if (canShoot && timeBtwShots <= 0)
        {
            if (facingRight)
            {
                SpawnBullet(transform.rotation);
            }
            else
            {
                SpawnBullet(Quaternion.Euler(new Vector3(0f, 0f, 180f)));
            }
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


    // retrieves the rigid body and turret controls on the player
    protected override void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
        turretController = GetComponent<TurretControlsAI>();
    }


    // spawns a bullet every time it can
    protected override void Update()
    {
        ShootBullet();
    }


    // moves the ai to a new designated location
    private void HeadTowards(Vector2 location, float direction)
    {
        if (direction > transform.position.x && !facingRight)
        {
            Flip();
        }
        if (direction < transform.position.x && facingRight)
        {
            Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, location, speed * Time.fixedDeltaTime);
    }

    // if not enough turrets, ai moves to place a turret, else it aims to kill the closest zombie, if no zombies places more turrets
    protected override void FixedUpdate()
    {
        // check for total turrets and if there are any enemies
        int totalTurrets = Physics2D.OverlapAreaAll(turrets1.position, turrets2.position, LayerMask.GetMask("Turrets")).Length;
        Transform closest = ClosestEnemy();
        // if theres too little turrets, move to add one, if theres enough target the closest enemy, and if there are no enemies place more turrets if possible
        if (totalTurrets < minTurrets && turretController.AffordCheapest())
        {
            GoToTurret();
        }
        else if (closest)
        {
            HeadTowards(new Vector2(transform.position.x, closest.position.y), closest.position.x);
        }
        else if (totalTurrets < TurretsPossible() && turretController.AffordCheapest())
        {
            GoToTurret();
        }
    }


    // finds closest enemy to 
    private Transform ClosestEnemy()
    {
        Transform closest = null; // closest enemy object
        float minDistance = -1f;  // closest distnce to an enemy
        Collider2D[] collisions = Physics2D.OverlapAreaAll(zombies1.position, zombies2.position, LayerMask.GetMask("Enemies", "Special Enemy")); // all enemies in area

        // finds newest closest enemy
        foreach (Collider2D coll in collisions)
        {
            if ( (Vector3.Distance(coll.GetComponent<Transform>().position, transform.position) < minDistance || minDistance == -1))
            {
                closest = coll.GetComponent<Transform>();
                minDistance = Vector3.Distance(closest.position, transform.position);
            }
        }

        return closest;
    }

    // aligns ai with where to put a turret
    private void GoToTurret()
    {
        Vector2 turretSpot = turretController.TurretLocation();
        if (turretSpot != Vector2.zero)
        {
            HeadTowards(new Vector2(turretSpot.x , turretSpot.y), turretSpot.x);
            if (transform.position.x == turretSpot.x && transform.position.y == turretSpot.y)
            {
                turretController.AllowPlacement();
            }
        }    
    }


    // calculates total amount of turrets that can be placed
    private int TurretsPossible()
    {
        int result = 0;
        Vector2 spaceBetween = grid.cellSize;
        int width = (int)Mathf.Abs(turrets2.position.x - turrets1.position.x);
        int height = (int)Mathf.Abs(turrets2.position.y - turrets1.position.y);
        for (int j = 0; j < width; j += (int)spaceBetween.x * 2)
        {
            for (int i = 0; i <= height; i += (int)spaceBetween.y * 2)
            {
                result++;
            }
        }
        return result;
    }

    
}
