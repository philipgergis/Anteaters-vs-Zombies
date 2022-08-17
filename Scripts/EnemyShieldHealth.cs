// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// shield zombies do not take damage first n times
public class EnemyShieldHealth : EnemyHealth
{

    [SerializeField] private float noDamage; // number of times zombie takes no damage at start
    private float noDamageLeft; // number of times zombie takes no damage left
    [SerializeField] private Image protection;


    // set noDamageLeft to noDamage
    protected override void Start()
    {
        base.Start();
        noDamageLeft = noDamage;
    }

    // deals damage if noDamage is 0 or less
    public override void TakeDamage(float amount, MoneyManager bank)
    {
        if(noDamageLeft <= 0)
        {
            base.TakeDamage(amount, bank);
        }
        else
        {
            ManageDamageLeft();
        }
    }

    private void ManageDamageLeft()
    {
        noDamageLeft--;
        float protectionRemaining = noDamageLeft / noDamage;
        protection.fillAmount = protectionRemaining;
    }
}
