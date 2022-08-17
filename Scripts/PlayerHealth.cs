// Amber

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// health related aspects involved with player
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 20f; // max health of player
     
    protected float currentHealth; // current health of player

    [SerializeField]
    private GameController healthBar; // script representing player health

    // sets players health to max
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // goes to game over scene when player health is 0 or below
    protected virtual void Update()
    {
        if (currentHealth <= 0)
        {
            GameStateManager.GameOverScene();
        }
    }

    // player takes damage and adjusts the image and current health as a result
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
