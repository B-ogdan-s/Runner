using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static System.Action _AddCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Animator>() != null)
        {
            Debug.Log("Coin");
            gameObject.SetActive(false);
            _AddCoin?.Invoke();
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
