// Amber and Philip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controls enemy health related aspects 
public class EnemyHealth : MonoBehaviour
{
    // health bar image
    [SerializeField]
    private Image healthBar;

    // enemy's starting health
    [SerializeField]
    private float enemyHealth;

    // enemy's current health
    private float currentHealth;

    // checks if object is alive
    private bool enemyAlive = true;

    // damage done by zombie
    [SerializeField]
    private float damage = 1f;

    // money on death
    [SerializeField]
    private int deathMoney = 100;

    protected virtual void Start()
    {
        currentHealth = enemyHealth;
    }

    public virtual void TakeDamage(float amount, MoneyManager bank)
    {
        // Reduce current health by the amount of damage done.
        currentHealth -= amount;

        // Change the UI elements appropriately.
        UpdateHealthBar();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (currentHealth <= 0f && enemyAlive)
        {
            OnDeath(bank);
        }
    }


    private void UpdateHealthBar()
    {
        // adjusts the fill of the health
        float health = currentHealth / enemyHealth;
        healthBar.fillAmount = health;
    }

    private void OnDeath(MoneyManager bank)
    {
        // makes enemy not alive, adds money's worth to bank, then deletes object

        enemyAlive = false;

        // finds money manager then uses its function
        bank.GetComponent<MoneyManager>().EditMoney(deathMoney);

        Destroy(gameObject);
    }

    // get damage it deals
    public float GetDamage()
    {
        return damage;
    }

    // on collision if target is a turret or player object, it takes damage
    private void OnCollisionStay2D(Collision2D collision)
    {

        // Find the health script associated with the rigidbody.
        Shooter targetHealth = collision.gameObject.GetComponent<Shooter>();
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (targetHealth)
        {
            targetHealth.TakeDamage(damage);
        }
        if (playerHealth)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
