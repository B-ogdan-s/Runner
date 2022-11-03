using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static System.Action _Death;

    private Coin _coin;
    private void Start()
    {
        _coin = GetComponentInChildren<Coin>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Animator>() != null)
        {
            Debug.Log("Dead");
            _Death();
        }
    }

    private void OnEnable()
    {
        _coin?.Active();
    }

}
