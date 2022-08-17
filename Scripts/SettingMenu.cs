//Script Started By Amber
//Contributed By Amber, Alexander

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private bool GameIsPause = false;
    private bool SettingsOpen = false;
    [SerializeField]
    private GameObject SettingMenuButton;
    [SerializeField]
    private GameObject SettingMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            SettingMenuUI.SetActive(false);
            SettingMenuButton.SetActive(true);
        }
    }
    // pauses menu when settings pressed
    public void Pause()
    {
        SettingMenuButton.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        SettingsOpen = true;
    }

    // resumes game when settings is exited
    public void Resume()
    {
        SettingMenuButton.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        SettingsOpen = false;
    }
}
