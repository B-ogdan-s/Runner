using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas _login;
    [SerializeField] private Canvas _registory;

    private void Awake()
    {
        _login.enabled = false;
        _registory.enabled = false;
        Auth._OpenRegistory += OpenRegistory;
        Auth._CloseRegistory += CloseRegistory;
        Auth._CloseLogin += CloseLogin;
        Facebookauth._CloseUi += CloseRegistory;
    }

    public void OpenLogin()
    {
        _login.enabled = true;
    }
    public void CloseLogin()
    {
        _login.enabled = false;
    }

    public void OpenRegistory()
    {
        _registory.enabled=true;
    }
    public void CloseRegistory()
    {
        _registory.enabled = false;
    }

    public void NextScene()
    {
        SceneManager.LoadScene("PlayingScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        Auth._OpenRegistory -= OpenRegistory;
        Auth._CloseRegistory -= CloseRegistory;
        Auth._CloseLogin -= CloseLogin;
        Facebookauth._CloseUi -= CloseRegistory;
    }
}
