using Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private RoadInfo _roadInfo;
    [SerializeField] private float _speed;
    [SerializeField] private float _displacementTime;

    [SerializeField]private CharacterController _characterController;
    private Vector3 _vector = new Vector3(0, 0, 1);

    private Coroutine _coroutine;
    private Tween _tween;

    private float _acceleration = 0;

    private int _num = 0;
    private int _min;
    private int _max;

    private Queue<int> _numDisplacement = new Queue<int>();

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _min = -(_roadInfo._numRoads - 1) / 2;
        _max = _roadInfo._numRoads + (_min-1);
        Player.Player._Move += Move;
        Player.Player._Restart += PlayerRestart;
        Platform._Displacement += Displacement;

        Debug.Log("1");
    }
    public void PlayerRestart()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _tween.Kill(true);
        _numDisplacement.Clear();
        transform.DOMove(Vector3.zero,0);
        _num = 0;
        _acceleration = 0;
    }

    private void Move(float acceleration)
    {
        _acceleration += acceleration * Time.deltaTime;
        //_characterController.Move(transform.forward * (_speed + _acceleration) * Time.deltaTime);
        transform.localPosition += transform.forward * (_speed + _acceleration) * Time.deltaTime;
    }
    private void Displacement(int vector)
    {
        if (_num + vector >= _min && _num + vector <= _max)
        {
            _numDisplacement.Enqueue(vector);
        }
        if(_numDisplacement.Count == 1)
        {
            _coroutine = StartCoroutine(CR_Displacement());
        }

    }
    private IEnumerator CR_Displacement()
    {
        while(_numDisplacement.Count > 0)
        {
            int vector = _numDisplacement.Dequeue();
            _num += vector;

            float newPos = _num * _roadInfo._width;
            //Vector3 newPos = new Vector3(_num * _roadInfo._width, transform.position.y, transform.position.z);

            Debug.Log(_numDisplacement.Count);

            //transform.position = new Vector3(_num * _roadInfo._width, transform.position.y, transform.position.z);
            _tween = transform.DOLocalMoveX(newPos, _displacementTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(_displacementTime);

            //_tween.Kill(true);
        }
    }

    private void OnDestroy()
    {
        Platform._Displacement -= Displacement;
        Player.Player._Move -= Move;
        Player.Player._Restart -= PlayerRestart;

        Debug.Log("2");
    }
}
