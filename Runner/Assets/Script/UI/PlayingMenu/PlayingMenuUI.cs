using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayingMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas _startingMenu;
    [SerializeField] private Canvas _playingMenu;
    [SerializeField] private Canvas _pauseMenu;
    [SerializeField] private Canvas _deathingMenu;

    private void Awake()
    {
        StartingState._Enter += OpenStartingMenu;
        StartingState._Exit += CloseStartingMenu;
        RunningState._Enter += OpenPlayingMenu;
        RunningState._Exit += ClosePlayingMenu;
        DeathingState._Enter += OpenDeathingMenu;
        DeathingState._Exit += CloseDeathingMenu;

        _playingMenu.enabled = false;
        _startingMenu.enabled = false;
        _pauseMenu.enabled = false;
        _deathingMenu.enabled = false;
    }
    private void OpenStartingMenu()
    {
        _startingMenu.enabled = true;
    }
    private void CloseStartingMenu()
    {
        _startingMenu.enabled = false;
    }
    private void OpenPlayingMenu()
    {
        _playingMenu.enabled = true;
    }
    private void ClosePlayingMenu()
    {
        _playingMenu.enabled = false;
    }


    private void OpenDeathingMenu()
    {
        _deathingMenu.enabled = true;
    }
    private void CloseDeathingMenu()
    {
        _deathingMenu.enabled = false;
    }

    public void NexScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        StartingState._Enter -= OpenStartingMenu;
        StartingState._Exit -= CloseStartingMenu;
        RunningState._Enter -= OpenPlayingMenu;
        RunningState._Exit -= ClosePlayingMenu;
        DeathingState._Enter -= OpenDeathingMenu;
        DeathingState._Exit -= CloseDeathingMenu;
    }
}
