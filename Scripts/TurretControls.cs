// Amanda and Philip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Uses keys to toggle between different shooters
// places object in front of player

public class TurretControls : MonoBehaviour
{
    [SerializeField]
    protected int[] costs;

    [SerializeField]
    private GameObject[] uiElements; // ui elements of the turrets

    [SerializeField]
    protected GameObject[] placeableObjectPrefabs; // array of objects 

    protected int current = 0;  // index of current shooter

    [SerializeField]
    protected Transform turretPlace; // gets transform of player 

    [SerializeField]
    protected Grid grid; // grid in game

    protected int available = 1; // how many of the total turrets are available

    // buttons to place and scroll through turrets
    [SerializeField] private KeyCode place;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    // money manager to handle purchasing of turrets
    [SerializeField] protected MoneyManager bank;

    // ignore money for testing
    [SerializeField] protected bool ignoreMoney = false;

    // ignore locked permissions for testing
    [SerializeField] protected bool ignoreLock = false;

    protected virtual void Start()
    {
        if(ignoreLock)
        {
            available = placeableObjectPrefabs.Length;
        }
    }

    protected virtual void Update()
    {
        SelectTurret();
        PlaceTurret();
    }

    
    protected virtual void SelectTurret()
    {
        // disable UI for previous turret
        uiElements[current].SetActive(false);

        // select new turret
        if (Input.GetKeyDown(right))
        {
            current = current + 1 < available ? current + 1 : 0;
        }
        if(Input.GetKeyDown(left))
        {
            current = current - 1 >= 0 ? current - 1 : available-1;
        }

        // enable ui for new turret
        uiElements[current].SetActive(true);
    }

    protected virtual void PlaceTurret()
    {
        Vector2 size = placeableObjectPrefabs[0].transform.localScale; // gets half the size of the object being placed
        Collider2D body = Physics2D.OverlapBox(turretPlace.position, size, 0f, LayerMask.GetMask("Enemies", "Special Enemy", "Turret AOE", "Turret")); // checks if bodies of the new object and turret object are overlapping
        if (body == null && Input.GetKeyDown(place) && (AffordTurret() || ignoreMoney)) // keypad inputs to place objects and only places if there is no overlap
        {
            bank.EditMoney(-costs[current]);
            GameObject currentPlaceableObject = Instantiate(placeableObjectPrefabs[current]); // places object i
            Vector3Int cellPosition = grid.WorldToCell(turretPlace.position); // gets cell transform is on
            currentPlaceableObject.transform.position = grid.GetCellCenterWorld(cellPosition);  // spawns turret on cell
            currentPlaceableObject.GetComponent<Shooter>().SetBank(bank);  // sets bank for shooter
        }
    }

    protected bool AffordTurret()
    {
        return bank.GetMoney() >= costs[current];
    }

    // iterate to unlock next turret
    public void IterateAvailable()
    {
        if(available < placeableObjectPrefabs.Length)
        {
            available++;
        }
    }
}
