// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


// manages money spent and earned in the game
public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int totalMoney;  // amount of money player has

    private TextMeshProUGUI text; 
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // displays total money as text
    private void Update()
    {
        text.text = totalMoney.ToString();
    }

    // adds or subtracts from total money
    public void EditMoney(int amount)
    {
        totalMoney += amount;
    }

    // returns total money owned
    public int GetMoney()
    {
        return totalMoney;
    }
}
