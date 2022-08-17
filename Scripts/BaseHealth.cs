// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes it so when enemies reach the end of the screen, the game is lost
public class BaseHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject player; // player object with health on it


    private void OnTriggerStay2D(Collider2D collision)
    {
        // trigger collision with an enemy and the base causes the game to end
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(100f);
        }
    }
}
