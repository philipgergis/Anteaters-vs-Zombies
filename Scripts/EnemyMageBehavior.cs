// Philip Gergis and Alex

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy that shoots projectiles, spawns more enemies, and reacts differently based on a finite machine
public class EnemyMageBehavior : MonoBehaviour
{
    private float timeBtwSpawns; // time ebtween each spawning

    [SerializeField]
    private float startTimeBtwSpawns; // original time between shots

    [SerializeField]
    private GameObject projectile; // bullet prefab

    [SerializeField]
    private GameObject zombieClone; // zombie clone prefab

    [SerializeField]
    private GameObject zombieTrack; // zombie clone prefab

    [SerializeField]
    private Transform spawnZombie; // where zombie spawns clones

    [SerializeField]
    private Transform spawnBullet; // where zombie spawns bullet

    [SerializeField]
    private Transform spawnBulletInner; // where zombie spawns bullet

    [SerializeField]
    private Transform outer1; // first point rectangle for outer vision

    [SerializeField]
    private Transform outer2;  // second point rectangle for outer vision

    [SerializeField]
    private Transform inner1; // first point rectangle for inner vision

    [SerializeField]
    private Transform inner2;  // second point rectangle for inner vision

    [SerializeField]
    private Vector2 mapCorner1; // first corner of map

    [SerializeField]
    private Vector2 mapCorner2; // second corner of map

    [SerializeField]
    private int minEnemies; // minimum enemies required so mage does not spawn more

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns; // iniates the time between spawns
    }

    private void Update()
    {
        // keeps subtracting timebtwspawn until it is less than 0, then spawns 2 objects again
        if (timeBtwSpawns <= 0)
        {
            determineBehavior();
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }


    // determines behavior based on whether player is near or not
    private void determineBehavior()
    {
        Collider2D playerExists = Physics2D.OverlapArea(outer1.position, outer2.position, LayerMask.GetMask("Players")); // gets a player object in whole vision
        if (playerExists)
        {
            playerDetected();
        }
        else
        {
            noDetection();
        }
    }

    // if player is not detected, mage spawns enemies when there are only 4 or less enemies on screen, otherwise it fires a bullet
    private void noDetection()
    {
        int totalEnemies = Physics2D.OverlapAreaAll(mapCorner1, mapCorner2, LayerMask.GetMask("Enemies", "Special Enemy")).Length; // gets all  enemies
        if (totalEnemies >= minEnemies)
        {
            SpawnObject(projectile, spawnBullet, Quaternion.identity);
        }
        else
        {
            SpawnObject(zombieClone, spawnZombie, Quaternion.identity);
        }
    }

    // if the player is detected on the outer ring, the mage spawns tracking zombies, otherwise it shoots 5 bullets behind it
    private void playerDetected()
    {
        Collider2D playerExistsBehind = Physics2D.OverlapArea(inner1.position, inner2.position, LayerMask.GetMask("Players")); // gets a player object in back vision
        if(playerExistsBehind)
        {
            int angleZ = 90;
            for (int i = 0; i < 5; i++)
            {
                SpawnObject(projectile, spawnBulletInner, Quaternion.identity * Quaternion.Euler(0, 0, angleZ));
                angleZ += 45;
            }
        }
        else
        {
            SpawnObject(zombieTrack, spawnZombie, Quaternion.identity);
        }
    }

    // Spawns an object and resets the timer for spawning
    private void SpawnObject(GameObject entity, Transform spot, Quaternion rotation)
    {
        Instantiate(entity, spot.position, rotation);
        timeBtwSpawns = startTimeBtwSpawns;
    }
}
