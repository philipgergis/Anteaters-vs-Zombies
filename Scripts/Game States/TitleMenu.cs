//Script Started By Alexander
//Contributed By Alexander, 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject InfoManagerHere;

    //Starts game in single player
    public void OnClick_SingleButton()
    {
        GameStateManager.DemoSolo();
    }

    //Starts game in Versus
    public void OnClick_VersusButton()
    {
        GameStateManager.DemoVersus();
    }

    //Starts game in multiplayer
    public void OnClick_CoOp()
    {
        GameStateManager.DemoCoop();
    }

    //Opens Settings
    public void OnClick_OpenInfo()
    {
        InfoManagerHere.SetActive(true);
    }

    //CLoses Settings
    public void OnClick_CloseInfo()
    {
        InfoManagerHere.SetActive(false);
    }

    //Quits game in build
    public void OnClick_ExitGame()
    {
        Application.Quit();
    }
}