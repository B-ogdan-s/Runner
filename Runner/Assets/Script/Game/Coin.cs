using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public static System.Action _AddCoin;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        float rot = Random.Range(0f, 360f);
        transform.DOLocalRotate(new Vector3(90, rot, 0), 0);
        _coroutine = StartCoroutine(CR_CoinAnim());
    }

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

    private IEnumerator CR_CoinAnim()
    {
        while (true)
        {
            transform.DOLocalRotate(new Vector3(90, 360, 0), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

}
