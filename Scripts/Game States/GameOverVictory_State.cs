//Script Started By Alexander
//Contributed By Alexander, 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverVictory_State : MonoBehaviour
{
    //[SerializeField] private string SceneNameForRestart;
    [SerializeField] private float delayTimerForButton;//Button appear after...seconds
    [SerializeField] private float delayTimerForSFX;//SFX plays after...seconds
    [SerializeField] private GameObject activeRestartButton;//UI "Restart" button
    [SerializeField] private GameObject activeMainMenuButton;//UI "Main Menu" button
    [SerializeField] private AudioClip SFX;//SFX file used in scene
    [SerializeField] private AudioSource audioSource;//Audio Source used in scene

    private void Start()
    {
        StartCoroutine(SFXActivatorTimer());//First count down for playing SFX
        StartCoroutine(ButtonActivatorTimer());//Second count down for showing buttons
    }

    private IEnumerator SFXActivatorTimer()
    {
        yield return new WaitForSeconds(delayTimerForSFX);
        audioSource.PlayOneShot(SFX);
    }

    private IEnumerator ButtonActivatorTimer()
    {
        yield return new WaitForSeconds(delayTimerForButton);
        activeMainMenuButton.SetActive(true);
        activeRestartButton.SetActive(true);
    }

    public void OnClick_ToMainMenu()
    {
        GameStateManager.MainMenu();
    }
    public void OnClick_Restart()
    {
        GameStateManager.Restart();
    }
}
