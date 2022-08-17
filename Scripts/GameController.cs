//Amber & Amanda

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// deals with player and game health/state
public class GameController : MonoBehaviour
{
    [SerializeField]
    private Slider slider; // slider being edited

    [SerializeField]
    private Gradient gradient; // gradient variable

    [SerializeField]
    private Image fill; // image that is being filled

    // sets the fill of the image to be equal to the max health
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    // sets the fill to be equal to the ratio of the current health to the max health
    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
