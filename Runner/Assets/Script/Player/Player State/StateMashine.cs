using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMashine
{
    private PlayerState _state;

    public StateMashine(PlayerState state)
    {
        _state = state;
        _state.Enter();
    }
    public void SetState(PlayerState state)
    {
        _state?.Exit();
        _state = state;
        _state.Enter();
    }
}
