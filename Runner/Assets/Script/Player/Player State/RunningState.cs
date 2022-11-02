using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    public static System.Action _Enter;
    public static System.Action _Exit;
    public override void Enter()
    {
        _Enter?.Invoke();
    }

    public override void Exit()
    {
        _Exit?.Invoke();
    }
}
