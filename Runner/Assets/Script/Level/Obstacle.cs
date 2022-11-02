using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static System.Action _Death;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Animator>() != null)
        {
            Debug.Log("Dead");
            _Death();
        }
    }
}
