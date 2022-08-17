// Philip Gergis and Caleb

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows the player to select different bullets
public class BulletSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiElements; // ui elements of the turrets

    [SerializeField]
    protected GameObject[] bullets; // list of bullets to select

    protected int current = 0;  // index of current bullet

    [SerializeField] private KeyCode left;  // previous bullet

    [SerializeField] private KeyCode right;  // next bullet

    protected PlayerControls pc; // player control script

    protected int available = 1; // how many total bullets are available

    // ignore locked permissions for testing
    [SerializeField] protected bool ignoreLock = false;



    private void Start()
    {
        if (ignoreLock)
        {
            available = bullets.Length;
        }
        pc = GetComponent<PlayerControls>(); // get player control script
    }

    protected virtual void Update()
    {
        // disable UI for previous turret
        uiElements[current].SetActive(false);

        if (Input.GetKeyDown(right)) // keypad inputs to place objects 
        {
            current = current + 1 < available ? current + 1 : 0;
        }
        if (Input.GetKeyDown(left))
        {
            current = current - 1 >= 0 ? current - 1 : available - 1;
        }

        // enable UI for new turret
        uiElements[current].SetActive(true);

        // set bullet
        pc.SetBullet(bullets[current]);
    }

    // iterates available to make another bullet available
    public void IterateAvailable()
    {
        if (available < bullets.Length)
        {
            available++;
        }
    }
}



