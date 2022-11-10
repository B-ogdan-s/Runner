using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    public static System.Action _Death;
    public static System.Action _AddCoin;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Obstacle>()!= null)
        {
            _Death?.Invoke();
        }
        if(other.GetComponent<Coin>()!= null)
        {
            _audioSource.Play();
            Debug.Log("Coin");
            other.gameObject.SetActive(false);
            _AddCoin?.Invoke();
}
    }
}
