using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _pauseCanvas;

    public void OpenPause()
    {
        Time.timeScale = 0f;
        _pauseCanvas.enabled = true;
    }
    public void ClosePause()
    {
        Time.timeScale = 1f;
        _pauseCanvas.enabled = false;
    }
}
