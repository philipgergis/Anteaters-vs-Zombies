//Script Started By Alexander
//Contributed By Alexander, Amber, Philip

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameStateManager : MonoBehaviour
{
    [SerializeField] //Single Player Mode
    private string m_SinglePlayerMode;
    [SerializeField] //Player vs AI Mode
    private string m_VersusMode;
    [SerializeField] //Multiplayer Mode
    private string m_MultiplayerMode;
    [SerializeField] //Main Menu Screen
    private string m_TitleSceneName;
    [SerializeField] //Win State Screen
    private string m_VictoryScene;
    [SerializeField] //Lose State Screen
    private string m_GameOverScene;

    enum GameStates { Solo, Multi, Versus } // enum for current game mode
    private static GameStates m_State; // enum varible assigned current game mode


    private static GameStateManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }


    // Each function loads their respective scene
    public static void DemoSolo()
    {
        m_State = GameStates.Solo;
        SceneManager.LoadScene(_instance.m_SinglePlayerMode);
    }

    public static void DemoVersus()
    {
        m_State = GameStates.Versus;
        SceneManager.LoadScene(_instance.m_VersusMode);
    }

    public static void DemoCoop()
    {
        m_State = GameStates.Multi;
        SceneManager.LoadScene(_instance.m_MultiplayerMode);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(_instance.m_TitleSceneName);
    }
    public static void VictoryScene()
    {
        SceneManager.LoadScene(_instance.m_VictoryScene);
    }
    public static void GameOverScene()
    {
        SceneManager.LoadScene(_instance.m_GameOverScene);
    }
    public static void Restart()
    {
        if (m_State == GameStates.Multi)
        {
            DemoCoop();
        }
        else if (m_State == GameStates.Solo)
        {
            DemoSolo();
        }
        else
        {
            DemoVersus();
        }
    }
}