// Amber

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// manages the waves, including which enemies spawn, how many spawn, how often they spawn, ect
[System.Serializable]
public class Wave
{
    public string waveName; // name of the wave
    public int noOfEnemies; // total number of enemies
    public GameObject[] typeOfEnemies; // types of enemies
    public float spawnInterval; // spawn interval betweene each enemy
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves; // list of all waves
    [SerializeField] private Transform[] spawnPoints; // where enemies can spawn
    [SerializeField] private Animator animator; // animating wave sequence
    [SerializeField] private TextMeshProUGUI waveName; // wave name made into a text mesh pro
    [SerializeField] private GameObject Unlock;

    private Wave currentWave; // current wave happening with its info

    private int currentWaveNumber; // current wave number from index

    private float nextSpawnTime; // next wave time spawn

    private bool canSpawn = true; // whether enemies can be spawned
    private bool canAnimate = false; // whether animating next wave sequence can happen

    //goes to next wave when enemies are all killed
    private void Update()
    {
        currentWave = waves[currentWaveNumber];

        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        // checks if enemies are all dead
        if (totalEnemies.Length == 0 && currentWave.noOfEnemies == 0)
        {
            // if not final wave, game continues, if final wave finished, game ends
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else
            {
                GameStateManager.VictoryScene();
            }
        }

    }



    // spawns next wave by iterating wave number and making can spawn true
    void SpawnNextWave()
    {
        // unlocks new turret and bullet after every 2 waves are completed
        if (currentWaveNumber % 2 == 1)
        {
            UnlockFeatures();

            Unlock.SetActive(true);

            StartCoroutine(RemoveAfterSeconds(3, Unlock));

            
            //Destroy(Unlock, 2);

            
        }

        currentWaveNumber++;
        canSpawn = true;
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject Unlock)
    {
        yield return new WaitForSeconds(seconds);
        Unlock.SetActive(false);
    }

    // spawns the actual wave by spawning enemies after a nextspawntime is less than the time
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }

    // unlocks next turret and bullet
    void UnlockFeatures()
    {
        GameObject[] totalPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in totalPlayers)
        {
            // makes sure the object has turret and bullet scripts
            TurretControls tc = player.GetComponent<TurretControls>();
            BulletSelection bs = player.GetComponent<BulletSelection>();

            // unlocks the next turret and bullet through iteration
            if(tc)
            {
                tc.IterateAvailable();
            }
            if (bs)
            {
                bs.IterateAvailable();
            }
        }
    }
}
