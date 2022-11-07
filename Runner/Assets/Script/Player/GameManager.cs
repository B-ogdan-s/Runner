using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private StateMashine _statemashine;

    private bool _deathCheck = true;

    private void Start()
    {
        _statemashine = new StateMashine(new StartingState());
        Obstacle._Death += Death;
        _deathCheck = true;
    }

    public void PlayStart()
    {
        _statemashine.SetState(new RunningState());
    }

    private void Death()
    {
        if (_deathCheck)
        {
            _deathCheck = false;
            _statemashine.SetState(new DeathingState());
        }
    }

    public void Restart()
    {
        _deathCheck = true;
        _statemashine.SetState(new StartingState());
    }

    private void OnDestroy()
    {
        Obstacle._Death -= Death;
    }
}
