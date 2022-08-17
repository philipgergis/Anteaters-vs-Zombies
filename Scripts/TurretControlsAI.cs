// Philip Gergis and Alex

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ai script to determine where to place turrets and which turrets to place
public class TurretControlsAI : TurretControls
{

    // points for the rectangle where ai can place turrets
    [SerializeField] private Transform turrets1;
    [SerializeField] private Transform turrets2;

    // ints for tracking the turret coordinates
    private float startX, endX, startY, endY;


    // assign turret coordinate values
    protected override void Start()
    {
        base.Start();
        startX = (turrets1.position.x < turrets2.position.x ? turrets1.position.x : turrets2.position.x);
        endX = (turrets1.position.x < turrets2.position.x ? turrets2.position.x : turrets1.position.x);
        startY = (turrets1.position.y < turrets2.position.y ? turrets1.position.y : turrets2.position.y);
        endY = (turrets1.position.y < turrets2.position.y ? turrets2.position.y : turrets1.position.y);
    }


    // let other entities check if any turret can be placed
    public bool AffordCheapest()
    {
        return bank.GetMoney() >= costs[0];
    }

    // get position where AI wants to place a turret
    public Vector2 TurretLocation()
    {
        // find starting and ending rows/cols for turret spots
        Vector2 spaceBetween = grid.cellSize;

        // iterate through spots by cols first to find an available one
        for (float j = startX; j < endX; j += spaceBetween.x)
        {
            for (float i = startY; i <= endY; i += spaceBetween.y)
            {
                if (AvailableLocation(new Vector2(j, i)))
                {
                    return new Vector2(j, i);
                }
            }
        }

        return Vector2.zero;
    }

    // checks if a spot is available for a turret
    public bool AvailableLocation(Vector2 location)
    {
        Vector2 size = placeableObjectPrefabs[0].transform.localScale; // gets half the size of the object being placed
        Collider2D body = Physics2D.OverlapBox(location, size, 0f, LayerMask.GetMask("Enemies", "Special Enemy", "Turret AOE", "Turret")); // checks if bodies of the new object and turret object are overlapping
        if (body)
        {
            return false;
        }
        return true;
    }

    // method to allow other methods to place turrets
    public void AllowPlacement()
    {
        PlaceTurret();
    }

    // selects turret
    protected override void Update()
    {
        SelectTurret();
    }


    // selects turret based off of the location from the end line
    protected override void SelectTurret()
    {
        if (turretPlace.position.x <= (startX + ((endX - startX) / 3f)))
        {
            DetermineTurret(new int[] { 3, 2, 1, 0 });
        }
        else if (turretPlace.position.x <= (startX + (2f * (endX - startX) / 3f)))
        {
            DetermineTurret(new int[] { 5, 2, 0 });
        }
        else
        {
            DetermineTurret(new int[] { 4, 2 });
        }
    }


    // places turret
    protected override void PlaceTurret()
    {
        Vector2 size = placeableObjectPrefabs[0].transform.localScale; // gets half the size of the object being placed
        Collider2D body = Physics2D.OverlapBox(turretPlace.position, size, 0f, LayerMask.GetMask("Enemies", "Special Enemy", "Turret AOE", "Turret")); // checks if bodies of the new object and turret object are overlapping
        if (body == null && (AffordTurret() || ignoreMoney)) // only places if there is no overlap and enough money
        {
            bank.EditMoney(-costs[current]);
            GameObject currentPlaceableObject = Instantiate(placeableObjectPrefabs[current]); // places object i
            Vector3Int cellPosition = grid.WorldToCell(turretPlace.position); // gets cell transform is on
            currentPlaceableObject.transform.position = grid.GetCellCenterWorld(cellPosition);  // spawns turret on cell
            currentPlaceableObject.GetComponent<Shooter>().SetBank(bank);  // sets bank for shooter
        }
    }


    // checks to see if the bullet is unlocked
    private bool CanUseTurret(int currentIndex)
    {
        if (currentIndex < available)
        {
            return true;
        }
        return false;
    }


    // goes through a list of bullets in descending order to determine the newest bullet the ai can use
    private void DetermineTurret(int[] turretIndices)
    {
        foreach (int i in turretIndices)
        {
            if (CanUseTurret(i) && bank.GetMoney() >= costs[i])
            {
                current = i;
                break;
            }
        }
        current = 0;
    }
}
