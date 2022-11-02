using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGround : MonoBehaviour
{
    public static System.Action _Check;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<RoadCheck>() != null)
        {
            _Check();
        }
    }
}
