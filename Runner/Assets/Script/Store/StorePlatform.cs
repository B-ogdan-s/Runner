using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StorePlatform : MonoBehaviour
{
    [SerializeField] private float _time;

    private Coroutine _coroutine;
    private Tween _tween;

    private void OnEnable()
    {
        StoreUI._Open += Open;
        StoreUI._Close += Close;
    }

    public void Open()
    {
        _coroutine = StartCoroutine(Rotate());
    }
    public void Close()
    {
        StopCoroutine(_coroutine);
        _tween.Kill();
        transform.DOLocalRotate(new Vector3(0, -90, 0), 0);
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            _tween = transform.DOLocalRotate(new Vector3(0, 360 + transform.localEulerAngles.y, 0), _time, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(_time);
        }
    }

    private void OnDisable()
    {

        StoreUI._Open -= Open;
        StoreUI._Close -= Close;
    }
}
