// Philip Gergis and Alex

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// scripts management movement and shooting controls
public class PlayerControls : MonoBehaviour
{
    // amount of time before player can shoot again
    protected float timeBtwShots;

    // the starting amount of time before player can shoot again
    [SerializeField]
    protected float startTimeBtwShots;

    // vector to get the horizontal axis and vertical axis
    private Vector2 movement;

    // field to keep the bullet object inside
    [SerializeField]
    protected GameObject bullet;

    // speed the player moves at in all directions
    [SerializeField]
    protected float speed = 30.0f;

    // rigid body of plyer
    protected Rigidbody2D playerBody;

    // checks which direction sprite is facing
    protected bool facingRight = true;

    // bullet offset from player
    [SerializeField]
    protected Transform shooterPosition;

    // horiztonal amd vertical axis name
    [SerializeField] private string axisH;
    [SerializeField] private string axisV;

    // shoot keycode
    [SerializeField] private KeyCode shootButton;

    // checks to see if player can shoot
    protected bool canShoot = true;

    [SerializeField] protected MoneyManager bank;  // bank used to set for bullets


    // shoots bullet depending on the way facing
    protected virtual void ShootBullet()
    {
        if (canShoot && timeBtwShots <= 0 && Input.GetKey(shootButton))
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


    // handles spawning of a bullet
    protected void SpawnBullet(Quaternion rotation)
    {
        GameObject tempBullet = Instantiate(bullet, shooterPosition.position, rotation);
        tempBullet.GetComponent<BulletBehavior>().SetBank(bank);
    }

    // flips the player sprite
    protected void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }



    // flips the player sprite to the direction it is heading
    private void DeterminePlayerDirection()
    {
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }



    // moves the player based on the WASD inputs and if the rigid body is registered
    private void MovePlayer()
    {
        DeterminePlayerDirection();
        playerBody.MovePosition(playerBody.position + movement * speed * Time.fixedDeltaTime);
    }



    // retrieves the rigid body on the player
    protected virtual void Start()
    {
        movement.x = Input.GetAxisRaw(axisH);
        movement.y = Input.GetAxisRaw(axisV);
        playerBody = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
    }



    // spawns a bullet every time shootButton is pressed and retrieve axises
    protected virtual void Update()
    {
        movement.x = Input.GetAxisRaw(axisH);
        movement.y = Input.GetAxisRaw(axisV);
        ShootBullet();
    }



    // used to move player
    protected virtual void FixedUpdate()
    {
        MovePlayer();
    }


    // multiply old fire rate
    public void EditFireRate(float rateMultiplier)
    {
        startTimeBtwShots *= rateMultiplier;
    }

    // edit speed of player
    public void EditSpeed(float speedChange)
    {
        speed *= speedChange;
    }

    // edit bullet being shot
    public void SetBullet(GameObject bulletSelection)
    {
        bullet = bulletSelection;
    }

    // change if player can shoot
    public void DetermineShoot(bool shoot)
    {
        canShoot = shoot;
    }
}
