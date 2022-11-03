using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathingState : PlayerState
{
    public static System.Action _Enter;
    public static System.Action _Exit;
    public override void Enter()
    {
        Debug.Log("DeathEna");
        _Enter?.Invoke();
    }

    public override void Exit()
    {

        Debug.Log("DeathDi");
        _Exit?.Invoke();
    }
}
