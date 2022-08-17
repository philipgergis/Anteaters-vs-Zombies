// Philip Gergis and Amber

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ai to select bullets
public class BulletSelectionAI : BulletSelection
{
    [SerializeField]
    private Transform outer1; // first point rectangle for outer vision

    [SerializeField]
    private Transform outer2;  // second point rectangle for outer vision

    [SerializeField]
    private Transform inner1; // first point rectangle for inner vision

    [SerializeField]
    private Transform inner2;  // second point rectangle for inner vision

    [SerializeField]
    private Transform special1; // first point rectangle for vision on special zombies

    [SerializeField]
    private Transform special2;  // second point rectangle for vision on special zombies


    // selects bullet to use then sets it, does not need to activate a UI
    protected override void Update()
    {
        current = SelectBullet();
        pc.SetBullet(bullets[current]);
    }

    // based on proximity of closest enemy, if a mage is alive, and what bullets it has unlocked, ai chooses bullet to use
    private int SelectBullet()
    {
        if (InArea(special1, special2, new string[] {"Special Enemy" }))
        {
            return DetermineBullet(new int[] { 5, 3 });
        }
        else if (InArea(inner1, inner2, new string[] { "Enemies", "Special Enemy" }))
        {
            return DetermineBullet(new int[] { 4, 2, 1 });
        }
        else if (InArea(outer1, outer2, new string[] { "Enemies", "Special Enemy" }))
        {
            return DetermineBullet(new int[] { 1 });
        }
        else
        {
            return DetermineBullet(new int[] { 3, 2, 1 });
        }
    }


    // checks if objects in certain layers are in the rectangle made by the coordinates 
    private bool InArea(Transform cord1, Transform cord2, string[] layers)
    {
        Collider2D exists = Physics2D.OverlapArea(cord1.position, cord2.position, LayerMask.GetMask(layers));
        if(exists)
        {
            return true;
        }
        return false;
    }


    // checks to see if the bullet is unlocked
    private bool CanUseBullet(int bulletIndex)
    {
        if(bulletIndex < available)
        {
            return true;
        }
        return false;
    }


    // goes through a list of bullets in descending order to determine the newest bullet the ai can use
    private int DetermineBullet(int[] bulletIndices)
    {
        foreach (int i in bulletIndices)
        {
            if(CanUseBullet(i))
            {
                return i;
            }
        }
        return 0;
    }
}
