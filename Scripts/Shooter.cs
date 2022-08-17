// Amanda and Philip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// general shooter behavior, including health and shooting rates
public class Shooter : MonoBehaviour
{

    [SerializeField]
    private Image healthBar; // image for turret health

    protected float timeBtwShots; // time ebtween each shot

    [SerializeField]
    protected float startTimeBtwShots; // original time between shots

    [SerializeField]
    protected GameObject projectile; // bullet prefab

    [SerializeField]
    protected Transform ShooterFirePoint; // where shooter shoots from

    [SerializeField]
    private float turretHealth; // original turret health

    private float currentHealth; // current health of turret

    private bool alive = true; // if turret is alive

    protected MoneyManager bank;  // bank where money is sent to


    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        currentHealth = turretHealth;
    }


    protected virtual void Update()
    {
        // keeps subtracting timebtwshots until it is less than 0, then shoots again
        if (timeBtwShots <= 0)
        {
            SpawnBullet(ShooterFirePoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    protected void SpawnBullet(Vector3 pos, Quaternion rotation)
    {
        GameObject tempBullet = Instantiate(projectile, pos, rotation);
        tempBullet.GetComponent<BulletBehavior>().SetBank(bank);
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        currentHealth -= amount;

        // Change the UI elements appropriately.
        UpdateHealthBar();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (currentHealth <= 0f && alive)
        {
            OnDeath();
        }
    }


    private void UpdateHealthBar()
    {
        // adjusts the fill of the health
        float health = currentHealth / turretHealth;
        healthBar.fillAmount = health;
    }

    protected virtual void OnDeath()
    {
        // when the turret hp is 0, the turret is destroyed
        alive = false;
        Destroy(gameObject);
    }

    public void SetBank(MoneyManager newBank)
    {
        bank = newBank;
    }
}