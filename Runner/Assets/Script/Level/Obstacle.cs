using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Coin _coin;
    private void Start()
    {
        _coin = GetComponentInChildren<Coin>();
    }

    private void OnEnable()
    {
        _coin?.Active();
    }

}
