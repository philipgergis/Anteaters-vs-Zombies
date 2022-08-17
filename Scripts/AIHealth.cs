// Philip Gergis and Amanda

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// edits the function of the AI's health script so when it dies the player wins
public class AIHealth : PlayerHealth
{
    protected override void Update()
    {
        if (currentHealth <= 0)
        {
            GameStateManager.VictoryScene();
        }
    }
}
